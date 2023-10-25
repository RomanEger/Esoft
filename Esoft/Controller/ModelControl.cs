using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Esoft.Controller;
using Esoft.Model;
using Esoft.View.Pages;
using NUnit.Framework;

namespace WpfApp1.Controller
{
    class ModelControl
    {
        public ModelControl()
        {
            esoftDB = new esoftDBEntities();
            parser = new Parser();
            isUniqueObj = new IsUnique();
        }

        public static esoftDBEntities esoftDB;

        protected Parser parser;

        readonly IsUnique isUniqueObj;

        protected class IsUnique
        {
            readonly private Parser parser = new Parser();

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
            finally
            {
                
            }
        }

        public async Task AddEstate(string data, int idTypeOfEstate)
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
                            }
                            break;
                        default:
                            break;
                    }
                await SaveChangesDB();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка");
            }
        }

        public async Task AddEstate(string[][] dataArr, int idTypeOfEstate)
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
                        }
                        break;
                    default:
                        break;
                }
                await SaveChangesDB();
                
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка");
            }
        }





        public async Task AddDemand(string data)
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
            await SaveChangesDB();
        }

        public async Task AddDemand(string[][] dataArr, int idTypeOfEstate)
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
                            await SaveChangesDB();
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
                            await SaveChangesDB();
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
                            await SaveChangesDB();
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





        public async Task AddOffer(string data)
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
            await SaveChangesDB();
        }

        public async Task AddOffer(string[][] dataArr)
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
                await SaveChangesDB();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
            
        }





        public async Task AddRealtor(string data)
        {

        }


        public async Task AddClient(string data)
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
                    await SaveChangesDB();
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка");
            }
        }

        public async Task UpdateClient(IEnumerable data)
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
            esoftDB.Clients.AddOrUpdate(clients);
            await SaveChangesDB();
        }
        
        public async Task RemoveClient(IEnumerable data)
        {
            try
            {
                Client[] clients = data.Cast<Client>().ToArray();
                foreach (var client in clients)
                {
                    var cl = await esoftDB.Clients.Where(x => x.Id == client.Id).FirstAsync();
                    esoftDB.Clients.Remove(cl);
                }
                await SaveChangesDB();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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

        public async Task<List<Realtor>> GetRealtors()
        {
            try 
            { 
                List<Realtor> list = await esoftDB.Realtors.ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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