using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Esoft.Model;

namespace WpfApp1.Controller
{
    class ModelControl
    {
        public static esoftDBEntities esoftDB;

        protected class IsUnique
        {
            public async Task<bool> Estate(string[] data)
            {
                string cityAddress, streetAddress, houseNumber, apartmentNumber;
                cityAddress = GetStringOrNullData(data[1]);
                streetAddress = GetStringOrNullData(data[2]);
                houseNumber = GetStringOrNullData(data[3]);
                apartmentNumber = GetStringOrNullData(data[4]);

                int count = await esoftDB.Estates.Where(x =>
                    (x.CityAddress == cityAddress) &&
                    (x.StreetAddress == streetAddress) &&
                    (x.HouseNumber == houseNumber) &&
                    (x.ApartmentNumber == apartmentNumber)
                    ).CountAsync();
                if(count == 0)
                    return true;
                return false;
            }

            public async Task<bool> Client(string[] data)
            {
                string lastName, firstName, patronymic, email, mobileNumber;

                lastName = GetStringOrNullData(data[0]);
                firstName = GetStringOrNullData(data[1]);
                patronymic = GetStringOrNullData(data[2]);
                email = GetStringOrNullData(data[3]);
                mobileNumber = GetStringOrNullData(data[4]);

                int count = await esoftDB.Clients.Where(x =>
                    (x.LastName == lastName) &&
                    (x.FirstName == firstName) &&
                    (x.Patronymic == patronymic) &&
                    (x.Email == email) &&
                    (x.MobileNumber == mobileNumber)
                    ).CountAsync();

                bool isUniqueEmail = await ClientEmailAsync(email);

                bool isUnqiueMobileNumber = await ClientMobileNumberAsync(mobileNumber);

                if (count == 0)
                    return true;
                return false;
            }

            private async Task<bool> ClientEmailAsync(string email)
            {
                int count = await esoftDB.Clients.Where(x => x.Email == email).CountAsync();

                if(count == 0) 
                    return true;
                return false;
            }

            private async Task<bool> ClientMobileNumberAsync(string mobileNumber)
            {
                int count = await esoftDB.Clients.Where(x => x.MobileNumber == mobileNumber).CountAsync();

                if (count == 0)
                    return true;
                return false;
            }


            public async Task<bool> Realtor(string[] data)
            {
                string lastName, firstName, patronymic;

                lastName = GetStringOrNullData(data[1]);
                firstName = GetStringOrNullData(data[2]);
                patronymic = GetStringOrNullData(data[3]);
                int? commission = ViewControl.ConvertToIntOrNull(data[4], 0, 100);

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

        public async static void SaveChangesDB()
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

        public static string GetStringOrNullData(string data)
        {
            if (string.IsNullOrEmpty(data))
                return null;
            else
                return data;
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
                                    CityAddress = GetStringOrNullData(dataSplit[1]),
                                    StreetAddress = GetStringOrNullData(dataSplit[2]),
                                    HouseNumber = GetStringOrNullData(dataSplit[3]),
                                    ApartmentNumber = GetStringOrNullData(dataSplit[4]),
                                    Latitude = ViewControl.ConvertToDoubleOrNull(dataSplit[5], -90, 90),
                                    Longtitude = ViewControl.ConvertToDoubleOrNull(dataSplit[6], -180, 180),
                                    NumberOfStroyes = ViewControl.ConvertToIntOrNull(dataSplit[7], 0),
                                    TotalArea = ViewControl.ConvertToDoubleOrNull(dataSplit[8], 0),
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
                                    CityAddress = GetStringOrNullData(dataSplit[1]),
                                    StreetAddress = GetStringOrNullData(dataSplit[2]),
                                    HouseNumber = GetStringOrNullData(dataSplit[3]),
                                    ApartmentNumber = GetStringOrNullData(dataSplit[4]),
                                    Latitude = ViewControl.ConvertToDoubleOrNull(dataSplit[5], -90, 90),
                                    Longtitude = ViewControl.ConvertToDoubleOrNull(dataSplit[6], -180, 180),
                                    TotalArea = ViewControl.ConvertToDoubleOrNull(dataSplit[7], 0),
                                    NumberOfRooms = ViewControl.ConvertToIntOrNull(dataSplit[8], 0),
                                    FloorNumber = ViewControl.ConvertToIntOrNull(dataSplit[9], 0),
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
                                    CityAddress = GetStringOrNullData(dataSplit[1]),
                                    StreetAddress = GetStringOrNullData(dataSplit[2]),
                                    HouseNumber = GetStringOrNullData(dataSplit[3]),
                                    ApartmentNumber = dataSplit[4],
                                    Latitude = ViewControl.ConvertToDoubleOrNull(dataSplit[5], -90, 90),
                                    Longtitude = ViewControl.ConvertToDoubleOrNull(dataSplit[6], -180, 180),
                                    TotalArea = ViewControl.ConvertToDoubleOrNull(dataSplit[7], 0),
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
                                    CityAddress = GetStringOrNullData(dataArr[i][1]),
                                    StreetAddress = GetStringOrNullData(dataArr[i][2]),
                                    HouseNumber = GetStringOrNullData(dataArr[i][3]),
                                    ApartmentNumber = GetStringOrNullData(dataArr[i][4]),
                                    Latitude = ViewControl.ConvertToDoubleOrNull(dataArr[i][5], -90, 90),
                                    Longtitude = ViewControl.ConvertToDoubleOrNull(dataArr[i][6], -180, 180),
                                    NumberOfStroyes = ViewControl.ConvertToIntOrNull(dataArr[i][7], 0),
                                    TotalArea = ViewControl.ConvertToDoubleOrNull(dataArr[i][8], 0),
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
                                    CityAddress = GetStringOrNullData(dataArr[i][1]),
                                    StreetAddress = GetStringOrNullData(dataArr[i][2]),
                                    HouseNumber = GetStringOrNullData(dataArr[i][3]),
                                    ApartmentNumber = GetStringOrNullData(dataArr[i][4]),
                                    Latitude = ViewControl.ConvertToDoubleOrNull(dataArr[i][5], -90, 90),
                                    Longtitude = ViewControl.ConvertToDoubleOrNull(dataArr[i][6], -180, 180),
                                    TotalArea = ViewControl.ConvertToDoubleOrNull(dataArr[i][7], 0),
                                    FloorNumber = ViewControl.ConvertToIntOrNull(dataArr[i][8], 0),
                                    NumberOfRooms = ViewControl.ConvertToIntOrNull(dataArr[i][9], 0),
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
                                    CityAddress = GetStringOrNullData(dataArr[i][1]),
                                    StreetAddress = GetStringOrNullData(dataArr[i][2]),
                                    HouseNumber = GetStringOrNullData(dataArr[i][3]),
                                    ApartmentNumber = GetStringOrNullData(dataArr[i][4]),
                                    Latitude = ViewControl.ConvertToDoubleOrNull(dataArr[i][5], -90, 90),
                                    Longtitude = ViewControl.ConvertToDoubleOrNull(dataArr[i][6], -180, 180),
                                    TotalArea = ViewControl.ConvertToDoubleOrNull(dataArr[i][7], 0),
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
                                    CityAddress = GetStringOrNullData(dataArr[i][1]),
                                    StreetAddress = GetStringOrNullData(dataArr[i][2]),
                                    HouseNumber = GetStringOrNullData(dataArr[i][3]),
                                    ApartmentNumber = GetStringOrNullData(dataArr[i][4]),
                                    MinPrice = ViewControl.ConvertToIntOrNull(dataArr[i][5]),
                                    MaxPrice = ViewControl.ConvertToIntOrNull(dataArr[i][6]),
                                    IdRealtor = int.Parse(dataArr[i][7]),
                                    IdClient = int.Parse(dataArr[i][8]),
                                    MinNumbOfStroyes = ViewControl.ConvertToIntOrNull(dataArr[i][9]),
                                    MaxNumbOfStroyes = ViewControl.ConvertToIntOrNull(dataArr[i][10]),
                                    MinTotalArea = ViewControl.ConvertToDoubleOrNull(dataArr[i][11]),
                                    MaxTotalArea = ViewControl.ConvertToDoubleOrNull(dataArr[i][12]),
                                    MinNumbOfRooms = ViewControl.ConvertToIntOrNull(dataArr[i][13]),
                                    MaxNumbOfRooms = ViewControl.ConvertToIntOrNull(dataArr[i][14]),
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
                                    CityAddress = GetStringOrNullData(dataArr[i][1]),
                                    StreetAddress = GetStringOrNullData(dataArr[i][2]),
                                    HouseNumber = GetStringOrNullData(dataArr[i][3]),
                                    ApartmentNumber = GetStringOrNullData(dataArr[i][4]),
                                    MinPrice = ViewControl.ConvertToIntOrNull(dataArr[i][5]),
                                    MaxPrice = ViewControl.ConvertToIntOrNull(dataArr[i][6]),
                                    IdRealtor = int.Parse(dataArr[i][7]),
                                    IdClient = int.Parse(dataArr[i][8]),
                                    MinTotalArea = ViewControl.ConvertToDoubleOrNull(dataArr[i][9]),
                                    MaxTotalArea = ViewControl.ConvertToDoubleOrNull(dataArr[i][10]),
                                    MinNumbOfRooms = ViewControl.ConvertToIntOrNull(dataArr[i][11]),
                                    MaxNumbOfRooms = ViewControl.ConvertToIntOrNull(dataArr[i][12]),
                                    MinFloorNumber = ViewControl.ConvertToIntOrNull(dataArr[i][13]),
                                    MaxFloorNumber = ViewControl.ConvertToIntOrNull(dataArr[i][14]),
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
                                    CityAddress = GetStringOrNullData(dataArr[i][1]),
                                    StreetAddress = GetStringOrNullData(dataArr[i][2]),
                                    HouseNumber = GetStringOrNullData(dataArr[i][3]),
                                    ApartmentNumber = GetStringOrNullData(dataArr[i][4]),
                                    MinPrice = ViewControl.ConvertToIntOrNull(dataArr[i][5]),
                                    MaxPrice = ViewControl.ConvertToIntOrNull(dataArr[i][6]),
                                    IdRealtor = int.Parse(dataArr[i][7]),
                                    IdClient = int.Parse(dataArr[i][8]),
                                    MinTotalArea = ViewControl.ConvertToDoubleOrNull(dataArr[i][9]),
                                    MaxTotalArea = ViewControl.ConvertToDoubleOrNull(dataArr[i][10]),
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
                bool isUnique = await isUniqueObj.Client(dataArr);
                if ((data[0] > 48 || data[0] < 57) && isUnique)
                {
                    string email = GetStringOrNullData(dataArr[3]);
                    string mobileNumber = GetStringOrNullData(dataArr[4]);

                    if (email == null && mobileNumber == null)
                    {
                        MessageBox.Show("Необходимо ввести номер телефона или адрес электронной почты", "Ошибка");
                        return;
                    }

                    Client client = new Client()
                    {
                        LastName = GetStringOrNullData(dataArr[0]),
                        FirstName = GetStringOrNullData(dataArr[1]),
                        Patronymic = GetStringOrNullData(dataArr[2]),
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

    }
}
