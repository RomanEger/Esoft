using System;
using System.Collections.Generic;
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
        protected class IsUnique
        {
            public static bool Estate(string[] data)
            {
                string cityAddress, streetAddress, houseNumber, apartmentNumber;
                cityAddress = GetStringOrNullData(data[1]);
                streetAddress = GetStringOrNullData(data[2]);
                houseNumber = GetStringOrNullData(data[3]);
                apartmentNumber = GetStringOrNullData(data[4]);

                int count = esoftDB.Estates.Where(x =>
                    (x.CityAddress == cityAddress) &&
                    (x.StreetAddress == streetAddress) &&
                    (x.HouseNumber == houseNumber) &&
                    (x.ApartmentNumber == apartmentNumber)
                    ).Count();
                if(count == 0)
                    return true;
                return false;
            }

            public static bool Client(string[] data)
            {
                string lastName, firstName, patronymic, email, mobileNumber;

                lastName = GetStringOrNullData(data[1]);
                firstName = GetStringOrNullData(data[2]);
                patronymic = GetStringOrNullData(data[3]);
                email = GetStringOrNullData(data[4]);
                mobileNumber = GetStringOrNullData(data[5]);

                int count = esoftDB.Clients.Where(x =>
                    (x.LastName == lastName) &&
                    (x.FirstName == firstName) &&
                    (x.Patronymic == patronymic) &&
                    (x.Email == email) &&
                    (x.MobileNumber == mobileNumber)
                    ).Count();
                if (count == 0)
                    return true;
                return false;
            }

            public static bool Realtor(string[] data)
            {
                string lastName, firstName, patronymic;

                lastName = GetStringOrNullData(data[1]);
                firstName = GetStringOrNullData(data[2]);
                patronymic = GetStringOrNullData(data[3]);
                int? commission = ViewControl.ConvertToIntOrNull(data[4], 0, 100);

                int count = esoftDB.Realtors.Where(x =>
                    (x.LastName == lastName) &&
                    (x.FirstName == firstName) &&
                    (x.Patronymic == patronymic) &&
                    (x.Commission == commission)
                    ).Count();
                if (count == 0)
                    return true;
                return false;
            }
        }
        
        public static esoftDBEntities esoftDB;

        public static void SaveChangesDB()
        {
            try
            {
                esoftDB.SaveChanges();
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


        public void AddEstate(string data, int idTypeOfEstate)
        {
            try
            {
                string[] dataSplit = data.Split(',');
                switch (idTypeOfEstate)
                {
                    case 0: //house
                        {

                            if ((data[0] > 48 || data[0] < 57) && !IsUnique.Estate(dataSplit))
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

                            if ((data[0] > 48 || data[0] < 57) && !IsUnique.Estate(dataSplit))
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
                            if ((data[0] > 48 || data[0] < 57) && !IsUnique.Estate(dataSplit))
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

        public void AddEstate(string[][] dataArr, int idTypeOfEstate)
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
                                if (i > 0 && !IsUnique.Estate(dataArr[i]))
                                    continue;
                                else if (!IsUnique.Estate(dataArr[i]))
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
                                if (dataArr[i][0].ToCharArray()[0] < 48 || dataArr[i][0].ToCharArray()[0] > 57)
                                    continue;
                                if (i > 0 && !IsUnique.Estate(dataArr[i]))
                                    continue;
                                else if (!IsUnique.Estate(dataArr[i]))
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
                                if (i > 0 && !IsUnique.Estate(dataArr[i]))
                                    continue;
                                else if (!IsUnique.Estate(dataArr[i]))
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





        public void AddDemand(string data)
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

        public void AddDemand(string[][] dataArr)
        {
            for (int i = 0; i < dataArr.Length; i++)
            {
                if (dataArr[i][0].ToCharArray()[0] < 48 || dataArr[i][0].ToCharArray()[0] > 57)
                    continue;
                Demand demand = new Demand()
                {
                    CityAddress = dataArr[i][1],

                };
                esoftDB.Demands.Add(demand);
            }
            SaveChangesDB();
        }





        public void AddOffer(string data)
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

        public void AddOffer(string[][] dataArr)
        {
            for (int i = 0; i < dataArr.Length; i++)
            {
                if (dataArr[i][0].ToCharArray()[0] < 48 || dataArr[i][0].ToCharArray()[0] > 57)
                    continue;
                Offer offer = new Offer()
                {

                };
                esoftDB.Offers.Add(offer);
            }
            SaveChangesDB();
        }





        public void AddRealtor(string data)
        {

        }

        public void AddRealtor(string[][] dataArr)
        {

        }




        public void AddClient(string data)
        {
            try
            {
                string[] dataArr = data.Split(new char[]{',', ' '});
                if ((data[0] > 48 || data[0] < 57) && !IsUnique.Client(dataArr))
                {
                    string email = GetStringOrNullData(dataArr[4]);
                    string mobileNumber = GetStringOrNullData(dataArr[5]);

                    if (email == null && mobileNumber == null)
                    {
                        MessageBox.Show("Необходимо ввести номер телефона или адрес электронной почты", "Ошибка");
                        return;
                    }

                    Client client = new Client()
                    {
                        LastName = GetStringOrNullData(dataArr[1]),
                        FirstName = GetStringOrNullData(dataArr[2]),
                        Patronymic = GetStringOrNullData(dataArr[3]),
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

        public void AddClient(string[][] dataArr)
        {
            try
            {
                for (int i = 0; i < dataArr.Length; i++)
                {
                    if (dataArr[i][0].ToCharArray()[0] < 48 ||
                        dataArr[i][0].ToCharArray()[0] > 57)
                        continue;

                    if (i > 0 && !IsUnique.Client(dataArr[i]))
                        continue;
                    else if (!IsUnique.Client(dataArr[i]))
                        continue;


                    string email = GetStringOrNullData(dataArr[i][4]);
                    string mobileNumber = GetStringOrNullData(dataArr[i][5]);

                    if (email == null && mobileNumber == null)
                    {
                        MessageBox.Show("Необходимо ввести номер телефона или адрес электронной почты", "Ошибка");
                        continue;
                    }

                    Client client = new Client()
                    {
                        LastName = GetStringOrNullData(dataArr[i][1]),
                        FirstName = GetStringOrNullData(dataArr[i][2]),
                        Patronymic = GetStringOrNullData(dataArr[i][3]),
                        Email = email,
                        MobileNumber = mobileNumber
                    };
                    esoftDB.Clients.Add(client);
                }
                SaveChangesDB();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка");
            }
        }

    }
}
