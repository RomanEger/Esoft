using Esoft.Controller;
using Esoft.Model;
using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1.Controller
{
    class ModelControl
    {
        public ModelControl()
        {
            esoftDB = new esoftDBEntities();
            parser = new Parser();
        }

        public static esoftDBEntities esoftDB;

        protected Parser parser;

        public async Task<bool> IsUniqueEstate(string[] data)
        {
            string cityAddress, streetAddress, houseNumber, apartmentNumber;
            int? latitude, longitude;
            cityAddress = parser.GetStringOrNullData(data[1]);
            streetAddress = parser.GetStringOrNullData(data[2]);
            houseNumber = parser.GetStringOrNullData(data[3]);
            apartmentNumber = parser.GetStringOrNullData(data[4]);
            latitude = parser.ConvertToIntOrNull(data[5]);
            longitude = parser.ConvertToIntOrNull(data[6]);

            try
            {
                int count = await esoftDB.Estates.Where(x =>
                (x.CityAddress == cityAddress) &&
                (x.StreetAddress == streetAddress) &&
                (x.HouseNumber == houseNumber) &&
                (x.ApartmentNumber == apartmentNumber) &&
                (x.Latitude == latitude && x.Longtitude == longitude)
                ).CountAsync();
                if (count == 0)
                    return true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;

        }


        public async Task<bool> IsUniqueClient(string[] data, int idOperation)
        {
            string lastName, firstName, patronymic, email, mobileNumber;

            lastName = parser.GetStringOrNullData(data[0]);
            firstName = parser.GetStringOrNullData(data[1]);
            patronymic = parser.GetStringOrNullData(data[2]);
            email = parser.GetStringOrNullData(data[3]);
            mobileNumber = parser.GetStringOrNullData(data[4]);

            try
            {
                int count = await esoftDB.Clients.Where(x =>
                (x.LastName == lastName) &&
                (x.FirstName == firstName) &&
                (x.Patronymic == patronymic) &&
                (x.Email == email) &&
                (x.MobileNumber == mobileNumber)
                ).CountAsync();

                int countEmail = await ClientEmailAsync(email);

                int countMobileNumber = await ClientMobileNumberAsync(mobileNumber);


                if (idOperation == 0)
                {
                    if (count == 0 && countEmail == 0)
                        return true;
                }
                else if (idOperation == 1)
                    if (count == 1 || (countEmail <= 1 && countMobileNumber <= 1))
                        return true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Проблемы с базой данных");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        private async Task<int> ClientEmailAsync(string email)
        {
            if (email == null)
                return 0;
            int count = await esoftDB.Clients.Where(x => x.Email == email).CountAsync();

            return count;
        }

        private async Task<int> ClientMobileNumberAsync(string mobileNumber)
        {
            if (mobileNumber == null)
                return 0;
            int count = await esoftDB.Clients.Where(x => x.MobileNumber == mobileNumber).CountAsync();


            return count;
        }


        public async Task<bool> IsUniqueRealtor(string[] data, int idOperation)
        {
            string lastName, firstName, patronymic;

            lastName = parser.GetStringOrNullData(data[0]);
            firstName = parser.GetStringOrNullData(data[1]);
            patronymic = parser.GetStringOrNullData(data[2]);
            int? commission = parser.ConvertToIntOrNull(data[3], 0, 100);
            try
            {

                int count = await esoftDB.Realtors.Where(x =>
                    (x.LastName == lastName) &&
                    (x.FirstName == firstName) &&
                    (x.Patronymic == patronymic) &&
                    (x.Commission == commission)
                    ).CountAsync();
                if (idOperation == 1 || (count == 0 && idOperation == 0))
                    return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;

        }


        public async Task<bool> IsUniqueOffer(string[] data)
        {
            return true;
        }

        public async Task<bool> IsUniqueDemand(string[] data)
        {
            return true;
        }



        public async Task SaveChangesDB()
        {
            try
            {
                await esoftDB.SaveChangesAsync();
                MessageBox.Show("Изменения успешно сохранены.", "Успех");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        protected static int Minimum(int a, int b, int c) => (a = a < b ? a : b) < c ? a : c;

        protected static int LevenshteinDistance(string firstWord, string secondWord)
        {
            firstWord = firstWord.ToLower();
            secondWord = secondWord.ToLower();

            var n = firstWord.Length + 1;
            var m = secondWord.Length + 1;
            var matrixD = new int[n, m];

            const int deletionCost = 1;
            const int insertionCost = 1;

            for (var i = 0; i < n; i++)
            {
                matrixD[i, 0] = i;
            }

            for (var j = 0; j < m; j++)
            {
                matrixD[0, j] = j;
            }

            for (var i = 1; i < n; i++)
            {
                for (var j = 1; j < m; j++)
                {
                    var substitutionCost = firstWord[i - 1] == secondWord[j - 1] ? 0 : 1;

                    matrixD[i, j] = Minimum(matrixD[i - 1, j] + deletionCost,          // удаление
                                            matrixD[i, j - 1] + insertionCost,         // вставка
                                            matrixD[i - 1, j - 1] + substitutionCost); // замена
                }
            }

            return matrixD[n - 1, m - 1];
        }



    }
}
//public async Task AddDemand(string data)
//{
//    string[] dataArr = data.Split(',');
//    for (int i = 0; i < dataArr.Length; i++)
//    {
//        if (data[0] < 48 || data[0] > 57)
//            continue;
//        Demand demand = new Demand()
//        {


//        };
//        esoftDB.Demands.Add(demand);
//    }
//    await SaveChangesDB();
//}






//public async Task AddOffer(string data)
//{
//    string[] dataArr = data.Split(',');
//    for (int i = 0; i < dataArr.Length; i++)
//    {
//        if (data[0] < 48 || data[0] > 57)
//            continue;
//        Offer offer = new Offer()
//        {


//        };
//        esoftDB.Offers.Add(offer);
//    }
//    await SaveChangesDB();
//}







//public async Task AddClient(string data)
//{
//    try
//    {
//        string[] dataArr = data.Split(new char[]{',', ' '});
//        bool isUnique = await IsUniqueClient(dataArr, 0);
//        if ((data[0] > 48 || data[0] < 57) && isUnique)
//        {
//            string email = parser.GetStringOrNullData(dataArr[3]);
//            string mobileNumber = parser.GetStringOrNullData(dataArr[4]);

//            if (email == null && mobileNumber == null)
//            {
//                MessageBox.Show($"Клиента \"{dataArr[0]} {dataArr[1]} {dataArr[2]} {email} {mobileNumber} не удалось добавить\"\nПроверьте корректность номера телефона или email", "Ошибка");
//                return;
//            }

//            Client client = new Client()
//            {
//                LastName = parser.GetStringOrNullData(dataArr[0]),
//                FirstName = parser.GetStringOrNullData(dataArr[1]),
//                Patronymic = parser.GetStringOrNullData(dataArr[2]),
//                Email = email,
//                MobileNumber = mobileNumber
//            };
//            esoftDB.Clients.Add(client);
//            await SaveChangesDB();
//        }
//    }
//    catch(Exception e)
//    {
//        MessageBox.Show("Не удалось добавить клиента\nПроверьте введенные данные и повторите попытку", "Ошибка");
//    }
//}

//public void AddClient(string[][] dataArr)
//{
//    try
//    {
//        for (int i = 0; i < dataArr.Length; i++)
//        {
//            if (dataArr[i][0].ToCharArray()[0] < 48 ||
//                dataArr[i][0].ToCharArray()[0] > 57)
//                continue;

//            if (i > 0 && !IsUnique.Client(dataArr[i]))
//                continue;
//            else if (!IsUnique.Client(dataArr[i]))
//                continue;


//            string email = GetStringOrNullData(dataArr[i][4]);
//            string mobileNumber = GetStringOrNullData(dataArr[i][5]);

//            if (email == null && mobileNumber == null)
//            {
//                MessageBox.Show("Необходимо ввести номер телефона или адрес электронной почты", "Ошибка");
//                continue;
//            }

//            Client client = new Client()
//            {
//                LastName = GetStringOrNullData(dataArr[i][1]),
//                FirstName = GetStringOrNullData(dataArr[i][2]),
//                Patronymic = GetStringOrNullData(dataArr[i][3]),
//                Email = email,
//                MobileNumber = mobileNumber
//            };
//            esoftDB.Clients.Add(client);
//        }
//        SaveChangesDB();

//    }
//    catch (Exception e)
//    {
//        MessageBox.Show(e.Message, "Ошибка");
//    }
//}