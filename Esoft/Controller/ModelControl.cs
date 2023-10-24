using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Esoft.Controller;
using Esoft.Model;
using NUnit.Framework;

namespace WpfApp1.Controller
{
    class ModelControl
    {
        public static esoftDBEntities esoftDB = new esoftDBEntities();

        protected Parser parser = new Parser();

        protected class IsUnique
        {
            private Parser parser = new Parser();

            public async Task<bool> Estate(string[] data)
            {
                string cityAddress, streetAddress, houseNumber, apartmentNumber;
                cityAddress = parser.GetStringOrNullData(data[1]);
                streetAddress = parser.GetStringOrNullData(data[2]);
                houseNumber = parser.GetStringOrNullData(data[3]);
                apartmentNumber = parser.GetStringOrNullData(data[4]);

                
                try
                {
                    int count = await esoftDB.Estates.Where(x =>
                    (x.CityAddress == cityAddress) &&
                    (x.StreetAddress == streetAddress) &&
                    (x.HouseNumber == houseNumber) &&
                    (x.ApartmentNumber == apartmentNumber)
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


            public async Task<bool> Client(string[] data, int idOperation)
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

                    bool isUniqueEmail = await ClientEmailAsync(email);

                    bool isUniqueMobileNumber = await ClientMobileNumberAsync(mobileNumber);

                    bool isUniqueContacts = (email != null || mobileNumber != null) && isUniqueEmail == true && isUniqueMobileNumber == true;

                    if (idOperation == 0)
                    {
                        if (count == 0 && isUniqueContacts == true)
                            return true;
                    }
                    else if (idOperation == 1)
                        if (count == 1 || isUniqueContacts == true)
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

            private async Task<bool> ClientEmailAsync(string email)
            {
                if (email == null)
                    return true;
                int count = await esoftDB.Clients.Where(x => x.Email == email).CountAsync();

                if(count == 0) 
                    return true;
                return false;
            }

            private async Task<bool> ClientMobileNumberAsync(string mobileNumber)
            {
                if(mobileNumber == null)
                    return true;
                int count = await esoftDB.Clients.Where(x => x.MobileNumber == mobileNumber).CountAsync();

                if (count == 0)
                    return true;
                return false;
            }


            public async Task<bool> Realtor(string[] data)
            {
                string lastName, firstName, patronymic;

                lastName = parser.GetStringOrNullData(data[1]);
                firstName = parser.GetStringOrNullData(data[2]);
                patronymic = parser.GetStringOrNullData(data[3]);
                int? commission = parser.ConvertToIntOrNull(data[4], 0, 100);

                int count = await esoftDB.Realtors.Where(x =>
                    (x.LastName == lastName) &&
                    (x.FirstName == firstName) &&
                    (x.Patronymic == patronymic) &&
                    (x.Commission == commission)
                    ).CountAsync();
                if (count == 0)
                    return true;
                return false;
            }


            public async Task<bool> Offer(string[] data)
            {
                return true;
            }

            public async Task<bool> Demand(string[] data)
            {
                return true;
            }
        }
        
        IsUnique isUniqueObj = new IsUnique();

        public async void SaveChangesDB()
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

        public async void AddEstate(string data, int idTypeOfEstate)
        {
            try
            {
                string[] dataSplit = data.Split(',');
                switch (idTypeOfEstate)
                {
                    case 0: //house
                        {
                            bool isUnique = await isUniqueObj.Estate(dataSplit);
                            if ((data[0] > 48 || data[0] < 57) && !isUnique)
                            { 
                                Estate estate = new Estate()
                                {
                                    CityAddress = parser.GetStringOrNullData(dataSplit[1]),
                                    StreetAddress = parser.GetStringOrNullData(dataSplit[2]),
                                    HouseNumber = parser.GetStringOrNullData(dataSplit[3]),
                                    ApartmentNumber = parser.GetStringOrNullData(dataSplit[4]),
                                    Latitude = parser.ConvertToDoubleOrNull(dataSplit[5], -90, 90),
                                    Longtitude = parser.ConvertToDoubleOrNull(dataSplit[6], -180, 180),
                                    NumberOfStroyes = parser.ConvertToIntOrNull(dataSplit[7], 0),
                                    TotalArea = parser.ConvertToDoubleOrNull(dataSplit[8], 0),
                                    IdTypeOfEstate = 1
                                };
                                esoftDB.Estates.Add(estate);
                            }
                                
                            SaveChangesDB();
                        }
                        break;
                    case 1: //appartment
                        {
                            bool isUnique = await isUniqueObj.Estate(dataSplit);
                            if ((data[0] > 48 || data[0] < 57) && !isUnique)
                            {
                                Estate estate = new Estate()
                                {
                                    CityAddress = parser.GetStringOrNullData(dataSplit[1]),
                                    StreetAddress = parser.GetStringOrNullData(dataSplit[2]),
                                    HouseNumber = parser.GetStringOrNullData(dataSplit[3]),
                                    ApartmentNumber = parser.GetStringOrNullData(dataSplit[4]),
                                    Latitude = parser.ConvertToDoubleOrNull(dataSplit[5], -90, 90),
                                    Longtitude = parser.ConvertToDoubleOrNull(dataSplit[6], -180, 180),
                                    TotalArea = parser.ConvertToDoubleOrNull(dataSplit[7], 0),
                                    NumberOfRooms = parser.ConvertToIntOrNull(dataSplit[8], 0),
                                    FloorNumber = parser.ConvertToIntOrNull(dataSplit[9], 0),
                                    IdTypeOfEstate = 2
                                };
                                esoftDB.Estates.Add(estate);
                            }
                            SaveChangesDB();
                        }
                        break;
                    case 2: //land
                        {
                            bool isUnique = await isUniqueObj.Estate(dataSplit);
                            if ((data[0] > 48 || data[0] < 57) && !isUnique)
                            {
                                Estate estate = new Estate()
                                {
                                    CityAddress = parser.GetStringOrNullData(dataSplit[1]),
                                    StreetAddress = parser.GetStringOrNullData(dataSplit[2]),
                                    HouseNumber = parser.GetStringOrNullData(dataSplit[3]),
                                    ApartmentNumber = parser.GetStringOrNullData(dataSplit[4]),
                                    Latitude = parser.ConvertToDoubleOrNull(dataSplit[5], -90, 90),
                                    Longtitude = parser.ConvertToDoubleOrNull(dataSplit[6], -180, 180),
                                    TotalArea = parser.ConvertToDoubleOrNull(dataSplit[7], 0),
                                    IdTypeOfEstate = 3
                                };
                                esoftDB.Estates.Add(estate);
                            }
                            SaveChangesDB();
                        }
                        break;
                    default:
                        break;
                }


            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка");
            }
        }

        public async void AddEstate(string[][] dataArr, int idTypeOfEstate)
        {
            try
            {
                switch(idTypeOfEstate)
                {
                    case 0: //house
                        {
                            for (int i = 0; i < dataArr.Length; i++)
                            {
                                if (dataArr[i][0].ToCharArray()[0] < 48 || 
                                    dataArr[i][0].ToCharArray()[0] > 57)
                                    continue;

                                bool isUnique = await isUniqueObj.Estate(dataArr[i]);
                                if (i > 0 && !isUnique)
                                    continue;
                                Estate estate = new Estate()
                                {
                                    CityAddress = parser.GetStringOrNullData(dataArr[i][1]),
                                    StreetAddress = parser.GetStringOrNullData(dataArr[i][2]),
                                    HouseNumber = parser.GetStringOrNullData(dataArr[i][3]),
                                    ApartmentNumber = parser.GetStringOrNullData(dataArr[i][4]),
                                    Latitude = parser.ConvertToDoubleOrNull(dataArr[i][5], -90, 90),
                                    Longtitude = parser.ConvertToDoubleOrNull(dataArr[i][6], -180, 180),
                                    NumberOfStroyes = parser.ConvertToIntOrNull(dataArr[i][7], 0),
                                    TotalArea = parser.ConvertToDoubleOrNull(dataArr[i][8], 0),
                                    IdTypeOfEstate = 1
                                };
                                esoftDB.Estates.Add(estate);
                            }
                            SaveChangesDB();
                        }
                        break;
                    case 1: //appartment
                        {
                            for (int i = 0; i < dataArr.Length; i++)
                            {
                                if (dataArr[i][0].ToCharArray()[0] < 48 || 
                                    dataArr[i][0].ToCharArray()[0] > 57)
                                    continue;
                                bool isUnique = await isUniqueObj.Estate(dataArr[i]);
                                if (i > 0 && !isUnique)
                                    continue;
                                Estate estate = new Estate()
                                {
                                    CityAddress = parser.GetStringOrNullData(dataArr[i][1]),
                                    StreetAddress = parser.GetStringOrNullData(dataArr[i][2]),
                                    HouseNumber = parser.GetStringOrNullData(dataArr[i][3]),
                                    ApartmentNumber = parser.GetStringOrNullData(dataArr[i][4]),
                                    Latitude = parser.ConvertToDoubleOrNull(dataArr[i][5], -90, 90),
                                    Longtitude = parser.ConvertToDoubleOrNull(dataArr[i][6], -180, 180),
                                    TotalArea = parser.ConvertToDoubleOrNull(dataArr[i][7], 0),
                                    FloorNumber = parser.ConvertToIntOrNull(dataArr[i][8], 0),
                                    NumberOfRooms = parser.ConvertToIntOrNull(dataArr[i][9], 0),
                                    IdTypeOfEstate = 2
                                };
                                esoftDB.Estates.Add(estate);
                            }
                            SaveChangesDB();
                        }
                        break;
                    case 2: //land
                        {
                            for (int i = 0; i < dataArr.Length; i++)
                            {
                                if (dataArr[i][0].ToCharArray()[0] < 48 || dataArr[i][0].ToCharArray()[0] > 57)
                                    continue;
                                bool isUnique = await isUniqueObj.Estate(dataArr[i]);
                                if (i > 0 && !isUnique)
                                    continue;
                                Estate estate = new Estate()
                                {
                                    CityAddress = parser.GetStringOrNullData(dataArr[i][1]),
                                    StreetAddress = parser.GetStringOrNullData(dataArr[i][2]),
                                    HouseNumber = parser.GetStringOrNullData(dataArr[i][3]),
                                    ApartmentNumber = parser.GetStringOrNullData(dataArr[i][4]),
                                    Latitude = parser.ConvertToDoubleOrNull(dataArr[i][5], -90, 90),
                                    Longtitude = parser.ConvertToDoubleOrNull(dataArr[i][6], -180, 180),
                                    TotalArea = parser.ConvertToDoubleOrNull(dataArr[i][7], 0),
                                    IdTypeOfEstate = 3
                                };
                                esoftDB.Estates.Add(estate);
                            }
                            SaveChangesDB();
                        }
                        break;
                    default:
                        break;
                }
                
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка");
            }
        }





        public async void AddDemand(string data)
        {
            string[] dataArr = data.Split(',');
            for (int i = 0; i < dataArr.Length; i++)
            {
                if (data[0] < 48 || data[0] > 57)
                    continue;
                Demand demand = new Demand()
                {
                    

                };
                esoftDB.Demands.Add(demand);
            }
            SaveChangesDB();
        }

        public async void AddDemand(string[][] dataArr, int idTypeOfEstate)
        {
            try
            {
                switch (idTypeOfEstate)
                {
                    case 0: //house
                        {
                            for (int i = 0; i < dataArr.Length; i++)
                            {
                                if (dataArr[i][0].ToCharArray()[0] < 48 ||
                                    dataArr[i][0].ToCharArray()[0] > 57)
                                    continue;
                                bool isUnique = await isUniqueObj.Demand(dataArr[i]);
                                if (i > 0 && !isUnique)
                                    continue;
                                Demand demand = new Demand()
                                {
                                    CityAddress = parser.GetStringOrNullData(dataArr[i][1]),
                                    StreetAddress = parser.GetStringOrNullData(dataArr[i][2]),
                                    HouseNumber = parser.GetStringOrNullData(dataArr[i][3]),
                                    ApartmentNumber = parser.GetStringOrNullData(dataArr[i][4]),
                                    MinPrice = parser.ConvertToIntOrNull(dataArr[i][5]),
                                    MaxPrice = parser.ConvertToIntOrNull(dataArr[i][6]),
                                    IdRealtor = int.Parse(dataArr[i][7]),
                                    IdClient = int.Parse(dataArr[i][8]),
                                    MinNumbOfStroyes = parser.ConvertToIntOrNull(dataArr[i][9]),
                                    MaxNumbOfStroyes = parser.ConvertToIntOrNull(dataArr[i][10]),
                                    MinTotalArea = parser.ConvertToDoubleOrNull(dataArr[i][11]),
                                    MaxTotalArea = parser.ConvertToDoubleOrNull(dataArr[i][12]),
                                    MinNumbOfRooms = parser.ConvertToIntOrNull(dataArr[i][13]),
                                    MaxNumbOfRooms = parser.ConvertToIntOrNull(dataArr[i][14]),
                                    IdTypeOfEstate = 1
                                };
                                esoftDB.Demands.Add(demand);
                            }
                            SaveChangesDB();
                        }
                        break;
                    case 1: //appartment
                        {
                            for (int i = 0; i < dataArr.Length; i++)
                            {
                                if (dataArr[i][0].ToCharArray()[0] < 48 || dataArr[i][0].ToCharArray()[0] > 57)
                                    continue;
                                bool isUnique = await isUniqueObj.Demand(dataArr[i]);
                                if (i > 0 && !isUnique)
                                    continue;
                                Demand demand = new Demand()
                                {
                                    CityAddress = parser.GetStringOrNullData(dataArr[i][1]),
                                    StreetAddress = parser.GetStringOrNullData(dataArr[i][2]),
                                    HouseNumber = parser.GetStringOrNullData(dataArr[i][3]),
                                    ApartmentNumber = parser.GetStringOrNullData(dataArr[i][4]),
                                    MinPrice = parser.ConvertToIntOrNull(dataArr[i][5]),
                                    MaxPrice = parser.ConvertToIntOrNull(dataArr[i][6]),
                                    IdRealtor = int.Parse(dataArr[i][7]),
                                    IdClient = int.Parse(dataArr[i][8]),
                                    MinTotalArea = parser.ConvertToDoubleOrNull(dataArr[i][9]),
                                    MaxTotalArea = parser.ConvertToDoubleOrNull(dataArr[i][10]),
                                    MinNumbOfRooms = parser.ConvertToIntOrNull(dataArr[i][11]),
                                    MaxNumbOfRooms = parser.ConvertToIntOrNull(dataArr[i][12]),
                                    MinFloorNumber = parser.ConvertToIntOrNull(dataArr[i][13]),
                                    MaxFloorNumber = parser.ConvertToIntOrNull(dataArr[i][14]),
                                    IdTypeOfEstate = 2
                                };
                                esoftDB.Demands.Add(demand);
                            }
                            SaveChangesDB();
                        }
                        break;
                    case 2: //land
                        {
                            for (int i = 0; i < dataArr.Length; i++)
                            {
                                if (dataArr[i][0].ToCharArray()[0] < 48 || dataArr[i][0].ToCharArray()[0] > 57)
                                    continue;
                                bool isUnique = await isUniqueObj.Demand(dataArr[i]);
                                if (i > 0 && !isUnique)
                                    continue;
                                Demand demand = new Demand()
                                {
                                    CityAddress = parser.GetStringOrNullData(dataArr[i][1]),
                                    StreetAddress = parser.GetStringOrNullData(dataArr[i][2]),
                                    HouseNumber = parser.GetStringOrNullData(dataArr[i][3]),
                                    ApartmentNumber = parser.GetStringOrNullData(dataArr[i][4]),
                                    MinPrice = parser.ConvertToIntOrNull(dataArr[i][5]),
                                    MaxPrice = parser.ConvertToIntOrNull(dataArr[i][6]),
                                    IdRealtor = int.Parse(dataArr[i][7]),
                                    IdClient = int.Parse(dataArr[i][8]),
                                    MinTotalArea = parser.ConvertToDoubleOrNull(dataArr[i][9]),
                                    MaxTotalArea = parser.ConvertToDoubleOrNull(dataArr[i][10]),
                                    IdTypeOfEstate = 3
                                };
                                esoftDB.Demands.Add(demand);
                            }
                            SaveChangesDB();
                        }
                        break;
                    default:
                        break;
                }


            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка");
            }
        }





        public async void AddOffer(string data)
        {
            string[] dataArr = data.Split(',');
            for (int i = 0; i < dataArr.Length; i++)
            {
                if (data[0] < 48 || data[0] > 57)
                    continue;
                Offer offer = new Offer()
                {


                };
                esoftDB.Offers.Add(offer);
            }
            SaveChangesDB();
        }

        public async void AddOffer(string[][] dataArr)
        {
            try
            {
                for (int i = 0; i < dataArr.Length; i++)
                {
                    if (dataArr[i][0].ToCharArray()[0] < 48 ||
                        dataArr[i][0].ToCharArray()[0] > 57)
                        continue;
                    bool isUnique = await isUniqueObj.Offer(dataArr[i]);
                    if (i > 0 && !isUnique)
                        continue;
                    Offer offer = new Offer()
                    {
                        Price = int.Parse(dataArr[i][1]),
                        IdRealtor = int.Parse(dataArr[i][2]),
                        IdClient = int.Parse(dataArr[i][3]),
                        IdEstate = int.Parse(dataArr[i][4])
                    };
                    esoftDB.Offers.Add(offer);
                }
                SaveChangesDB();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
            
        }





        public async void AddRealtor(string data)
        {

        }


        public async void AddClient(string data)
        {
            try
            {
                string[] dataArr = data.Split(new char[]{',', ' '});
                bool isUnique = await isUniqueObj.Client(dataArr, 0);
                if ((data[0] > 48 || data[0] < 57) && isUnique)
                {
                    string email = parser.GetStringOrNullData(dataArr[3]);
                    string mobileNumber = parser.GetStringOrNullData(dataArr[4]);

                    if (email == null && mobileNumber == null)
                    {
                        MessageBox.Show("Необходимо ввести номер телефона или адрес электронной почты", "Ошибка");
                        return;
                    }

                    Client client = new Client()
                    {
                        LastName = parser.GetStringOrNullData(dataArr[0]),
                        FirstName = parser.GetStringOrNullData(dataArr[1]),
                        Patronymic = parser.GetStringOrNullData(dataArr[2]),
                        Email = email,
                        MobileNumber = mobileNumber
                    };
                    esoftDB.Clients.Add(client);
                    SaveChangesDB();
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка");
            }
        }

        public async void UpdateClient(IEnumerable data)
        {
            Client[] clients = data.Cast<Client>().ToArray();
            string[][] arr = new string[clients.Length][];
            for(int i = 0; i < clients.Length; i++)
            {
                arr[i] = new string[] { parser.GetStringOrNullData(clients[i].LastName),
                                        parser.GetStringOrNullData(clients[i].FirstName),
                                        parser.GetStringOrNullData(clients[i].Patronymic),
                                        parser.GetStringOrNullData(clients[i].Email),
                                        parser.GetStringOrNullData(clients[i].MobileNumber) };
                
                bool isUnique = await isUniqueObj.Client(arr[i], 1);
                if (isUnique == false)
                {
                    MessageBox.Show("Проверьте уникальность данных и повторите попытку", "Сохранения не приняты");
                    return;
                }
            }

            SaveChangesDB();
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

        public async Task<List<Realtor>> GetRealtors()
        {
            List<Realtor> list = await esoftDB.Realtors.ToListAsync();
            return list;
        }
    }
}

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