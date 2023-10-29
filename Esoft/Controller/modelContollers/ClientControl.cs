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
    internal class ClientControl : ModelControl
    {
        
        public async Task<int> AddClient(Client[] data)
        {
            try
            {
                string[][] dataArr = new string[data.Length][];
                for (int i = 0; i < data.Length; i++)
                {
                    string str = data[i].LastName + " " + data[i].FirstName + " " + data[i].Patronymic + " " + data[i].Email + " " + data[i].MobileNumber;
                    dataArr[i] = str.Split(' ');
                    bool isUnique = await IsUniqueClient(dataArr[i], 0);

                    if (isUnique == true)
                    {
                        string email = parser.GetStringOrNullData(dataArr[i][3]);
                        string mobileNumber = parser.GetStringOrNullData(dataArr[i][4]);

                        if (email == null && mobileNumber == null)
                        {
                            MessageBox.Show($"Клиента \"{dataArr[i][0]} {dataArr[i][1]} {dataArr[i][2]} {email} {mobileNumber}\" не удалось добавить\nПроверьте корректность номера телефона или email", "Ошибка");
                            return -1;
                        }

                        Client client = new Client()
                        {
                            LastName = parser.GetStringOrNullData(dataArr[i][0]),
                            FirstName = parser.GetStringOrNullData(dataArr[i][1]),
                            Patronymic = parser.GetStringOrNullData(dataArr[i][2]),
                            Email = email,
                            MobileNumber = mobileNumber
                        };
                        esoftDB.Clients.Add(client);
                        await SaveChangesDB();
                        return 0;
                    }
                }
                return -1;
            }
            catch (Exception e)
            {
                MessageBox.Show("Не удалось добавить клиента\nПроверьте введенные данные и повторите попытку", "Ошибка");
                return -1;
            }
        }

        public async Task AddOrUpdateClient(IEnumerable data)
        {
            try
            {
                List<Client> clients = data.Cast<Client>().ToList();
                string[][] arr = new string[clients.Count][];
                for (int i = 0; i < clients.Count; i++)
                {
                    arr[i] = new string[] { parser.GetStringOrNullData(clients[i].LastName),
                                            parser.GetStringOrNullData(clients[i].FirstName),
                                            parser.GetStringOrNullData(clients[i].Patronymic),
                                            parser.GetStringOrNullData(clients[i].Email),
                                            parser.GetStringOrNullData(clients[i].MobileNumber) };

                    bool isUnique = await IsUniqueClient(arr[i], 1);
                    if (isUnique == false)
                    {
                        MessageBox.Show("Проверьте уникальность данных и повторите попытку", "Сохранения не приняты");
                        return;
                    }
                }
                int x = await AddClient(clients.ToArray());

                if (x != 0)
                {
                    clients.RemoveAt(clients.Count - 1);
                    esoftDB.Clients.AddOrUpdate(clients.ToArray());
                    await SaveChangesDB();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить изменения", "Ошибка");
            }
        }

        public async Task RemoveClient(IEnumerable data)
        {
            try
            {
                Client[] clients = data.Cast<Client>().ToArray();
                List<string> errRemove = new List<string>();
                foreach (var client in clients)
                {
                    var cl = await esoftDB.Clients.Where(x => x.Id == client.Id).FirstAsync();
                    if (cl.Demands.Count > 0 || cl.Offers.Count > 0)
                    {
                        errRemove.Add($"{cl.Id} | {cl.LastName} {cl.FirstName} {cl.Patronymic}");
                        continue;
                    }
                    esoftDB.Clients.Remove(cl);
                }
                if (errRemove.Count == 1)
                    MessageBox.Show($"Клиента {errRemove[0]} нельзя удалить");
                else if (errRemove.Count > 1)
                {
                    string str = string.Empty;
                    for (int i = 0; i < errRemove.Count; i++)
                        str += $"{errRemove[i]}\n";
                    MessageBox.Show($"Клиентов нельзя удалить: {str}");
                }
                await SaveChangesDB();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось удалить клиента", "Ошибка");
            }
        }

        public async Task<List<Client>> GetClients()
        {
            try
            {
                List<Client> list = await esoftDB.Clients.ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
