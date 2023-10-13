using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Esoft.Model;

namespace WpfApp1.Controller
{
    class ModelControl
    {
        

        public void AddData(string data, int tableId)
        {

            try
            {
                string[] dataSplit = data.Split(',');
                int latitude;
                ViewControl.Convert(dataSplit[4], out latitude, -90);
                switch (tableId)
                {
                    case 0:
                        {
                            Estate estate = new Estate()
                            {
                                CityAddress = dataSplit[0],
                                StreetAddress = dataSplit[1],
                                HouseNumber = dataSplit[2],
                                ApartmentNumber = dataSplit[3],
                                //Latitude = ViewControl.Convert(dataSplit[4], out latitude),
                            };
                        }
                        break;
                    default:
                        break;
                }
            }
            catch(Exception ex)
            {

            }
        }

        public void AddData(string[][] dataArr, int tableId)
        {

            try
            {
                int latitude;
                //for(int i = 0; i < dataArr.Length; i++)
                //{
                //    ViewControl.Convert(dataArr[i][4], out latitude, -90, 90);

                //}
                switch (tableId)
                {
                    case 0:
                        {
                            for(int i = 0; i < dataArr.Length; i++)
                            {
                                Estate estate = new Estate()
                                {
                                    CityAddress = dataArr[i][1],
                                    StreetAddress = dataArr[i][2],
                                    HouseNumber = dataArr[i][3],
                                    ApartmentNumber = dataArr[i][4],
                                    Latitude = GetDoubleData(dataArr[i][5]),
                                    Longtitude = GetDoubleData(dataArr[i][6]),
                                    NumberOfStroyes = GetIntData(dataArr[i][7]),
                                    ApartmentArea = GetDoubleData(dataArr[i][8])
                                };
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public int? GetIntData(string data)
        {
            if(data == null ||
               string.IsNullOrEmpty(data))
            {
                MessageBox.Show("Пустая строка");
                return null;
            }
            else if(int.TryParse(data, out int x))
                return x;
            else
            {
                MessageBox.Show("Некорректные данные");
                return null;
            }
        }

        public double? GetDoubleData(string data)
        {
            if (data == null ||
               string.IsNullOrEmpty(data))
            {
                MessageBox.Show("Пустая строка");
                return null;
            }
            else if (double.TryParse(data, out double x))
                return x;
            else
            {
                MessageBox.Show("Некорректные данные");
                return null;
            }
        }
    }
}
