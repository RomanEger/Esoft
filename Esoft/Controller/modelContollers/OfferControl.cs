using Esoft.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Controller;

namespace Esoft.Controller
{
    internal class OfferControl : ModelControl
    {

        public async Task AddOffer(string[][] dataArr)
        {
            try
            {
                for (int i = 0; i < dataArr.Length; i++)
                {
                    if (dataArr[i][0].ToCharArray()[0] < 48 ||
                        dataArr[i][0].ToCharArray()[0] > 57)
                        continue;
                    bool isUnique = await IsUniqueOffer(dataArr[i]);
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

    }
}
