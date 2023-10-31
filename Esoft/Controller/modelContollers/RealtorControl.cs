using Esoft.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Controller;

namespace Esoft.Controller
{
    internal class RealtorControl : ModelControl
    {

        public async Task AddOrUpdateRealtor(IEnumerable data)
        {
            try
            {
                List<Realtor> realtors = data.Cast<Realtor>().ToList();
                string[][] arr = new string[realtors.Count][];
                for (int i = 0; i < realtors.Count; i++)
                {
                    arr[i] = new string[] { parser.GetStringOrNullData(realtors[i].LastName),
                                            parser.GetStringOrNullData(realtors[i].FirstName),
                                            parser.GetStringOrNullData(realtors[i].Patronymic),
                                            parser.GetStringOrNullData(realtors[i].Commission.ToString()) };

                    bool isUnique = await IsUniqueRealtor(arr[i], 1);
                    if (isUnique == false)
                    {
                        MessageBox.Show("Проверьте уникальность данных и повторите попытку", "Сохранения не приняты");
                        return;
                    }
                    if (arr[i][0] == null || arr[i][1] == null)
                    {
                        MessageBox.Show($"Риэлтора \"{arr[i][0]} {arr[i][1]} {arr[i][2]} {arr[i][3]}\" не удалось добавить\nПроверьте данные и повторите попытку", "Ошибка");
                        realtors.RemoveAt(i);
                    }
                }


                if (true)
                {
                    esoftDB.Realtors.AddOrUpdate(realtors.ToArray());
                    await SaveChangesDB();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить изменения", "Ошибка");
            }
        }

        public async Task RemoveRealtor(IEnumerable data)
        {
            try
            {
                Realtor[] realtors = data.Cast<Realtor>().ToArray();
                List<string> errRemove = new List<string>();
                foreach (var client in realtors)
                {
                    var cl = await esoftDB.Realtors.Where(x => x.Id == client.Id).FirstAsync();
                    if (cl.Demands.Count > 0 || cl.Offers.Count > 0)
                    {
                        errRemove.Add($"{cl.Id} | {cl.LastName} {cl.FirstName} {cl.Patronymic}");
                        continue;
                    }
                    esoftDB.Realtors.Remove(cl);
                }
                if (errRemove.Count == 1)
                    MessageBox.Show($"Риэлтора {errRemove[0]} нельзя удалить");
                else if (errRemove.Count > 1)
                {
                    string str = string.Empty;
                    for (int i = 0; i < errRemove.Count; i++)
                        str += $"{errRemove[i]}\n";
                    MessageBox.Show($"Риэлторов нельзя удалить: {str}");
                }
                await SaveChangesDB();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось удалить риэлтора", "Ошибка");
            }
        }

        public async Task<IEnumerable> GetRealtors()
        {
            try
            {
                IEnumerable list = await esoftDB.Realtors.ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable> SearchRealtors(string search)
        {
            try
            {
                List<Realtor> realtors = await esoftDB.Realtors.ToListAsync();

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < search.Length; i++)
                {
                    if (!char.IsLetter(search[i]))
                        sb.Append(search[i]);
                }
                string separator = sb.ToString();

                sb.Clear();

                List<Realtor> result = new List<Realtor>();

                string[] strArr = search.Split(separator.ToCharArray());


                for (int i = 0; i < realtors.Count; i++)
                {
                    if (strArr.Length == 1)
                    {
                        if (LevenshteinDistance(strArr[0], realtors[i].LastName) <= 3 ||
                            LevenshteinDistance(strArr[0], realtors[i].FirstName) <= 3 ||
                            LevenshteinDistance(strArr[0], realtors[i].Patronymic = realtors[i].Patronymic == null ? "" : realtors[i].Patronymic) <= 3)
                            result.Add(realtors[i]);
                    }
                    else if (strArr.Length == 2)
                    {
                        if (LevenshteinDistance(strArr[0], realtors[i].LastName) + LevenshteinDistance(strArr[1], realtors[i].FirstName) <= 3 ||
                            LevenshteinDistance(strArr[0], realtors[i].LastName) + LevenshteinDistance(strArr[1], realtors[i].Patronymic = realtors[i].Patronymic == null ? "" : realtors[i].Patronymic) <= 3 ||
                            LevenshteinDistance(strArr[0], realtors[i].FirstName) + LevenshteinDistance(strArr[1], realtors[i].Patronymic = realtors[i].Patronymic == null ? "" : realtors[i].Patronymic) <= 3)
                            result.Add(realtors[i]);
                    }
                    else if (strArr.Length == 3)
                    {
                        if (LevenshteinDistance(strArr[0], realtors[i].LastName) +
                            LevenshteinDistance(strArr[1], realtors[i].FirstName) +
                            LevenshteinDistance(strArr[2], realtors[i].Patronymic = realtors[i].Patronymic == null ? "" : realtors[i].Patronymic) <= 3)
                            result.Add(realtors[i]);
                    }
                }



                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
