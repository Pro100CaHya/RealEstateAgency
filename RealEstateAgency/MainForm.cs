using RealEstateAgency.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace RealEstateAgency
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Columns.Clear();

            this.dataGridView1.Columns.Add("Address", "Адрес");
            this.dataGridView1.Columns.Add("Area", "Площадь");
            this.dataGridView1.Columns.Add("Floor", "Этаж");

            this.dataGridView1.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dataGridView1.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dataGridView1.Columns[2].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            float startPrice = float.Parse(this.textBoxRangeFrom.Text);
            float endPrice = float.Parse(this.textBoxRangeTo.Text);
            string district = this.textBoxDistrict.Text;

            using (var context = new RealEstateAgencyContext())
            {
                var objects = (
                    from rltobj in context.RealtyObjects
                    join d in context.Districts on rltobj.DistrictId equals d.Id
                    where d.Name == district && rltobj.DistrictId == d.Id
                        && rltobj.Price >= startPrice && rltobj.Price <= endPrice
                    select new
                    {
                        Address = rltobj.Address,
                        Area = rltobj.Area,
                        Floor = rltobj.Floor,
                    }).ToList();

                foreach (var obj in objects)
                {
                    this.dataGridView1.Rows.Add(obj.Address, obj.Area, obj.Floor);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Columns.Clear();

            this.dataGridView1.Columns.Add("Surname", "Фамилия");
            this.dataGridView1.Columns.Add("Name", "Имя");
            this.dataGridView1.Columns.Add("Patronym", "Отчество");

            this.dataGridView1.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dataGridView1.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dataGridView1.Columns[2].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            int NumberOfRooms = int.Parse(this.textBoxNumOfRooms.Text);

            using (var context = new RealEstateAgencyContext())
            {
                var objects = (
                    from rlt in context.Realtors
                    from dl in context.Deals
                    from rltobj in context.RealtyObjects
                    where dl.RealtyObjectId == rltobj.Id
                        && rltobj.NumberOfRooms == NumberOfRooms
                        && dl.RealtorId == rlt.Id
                    select new
                    {
                        Surname = rlt.Surname,
                        Name = rlt.Name,
                        Patronym = rlt.Patronym,
                    }).Distinct().ToList();

                foreach (var obj in objects)
                {
                    this.dataGridView1.Rows.Add(obj.Surname, obj.Name, obj.Patronym);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Columns.Clear();

            this.dataGridView1.Columns.Add("Sum", "Сумма проданных объектов недвижимости");
            this.dataGridView1.Columns.Add("Diff", "Разница");
            this.dataGridView1.Columns.Add("Surname", "Фамилия риэлтора");

            this.dataGridView1.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dataGridView1.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dataGridView1.Columns[2].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            int floor = int.Parse(this.textBoxFloor.Text);

            using (var context = new RealEstateAgencyContext())
            {
                var objects = (
                    from rlt in context.Realtors
                    from dl in context.Deals
                    from rltobj in context.RealtyObjects
                    where dl.RealtyObjectId == rltobj.Id
                        && rltobj.Floor == floor
                        && dl.RealtorId == rlt.Id
                    select new
                    {
                        Address = rltobj.Address,
                        Diff = dl.Price - rltobj.Price,
                        Surname = rlt.Surname,
                    }).ToList();

                foreach (var obj in objects)
                {
                    this.dataGridView1.Rows.Add(obj.Address, obj.Diff, obj.Surname);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Columns.Clear();

            this.dataGridView1.Columns.Add("TotalSum", "Общая стоимость объектов недвижимости");

            this.dataGridView1.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            string district = this.textBoxDistrict1.Text;
            int numberOfRooms = int.Parse(this.textBoxNumberOfRooms1.Text);

            using (var context = new RealEstateAgencyContext())
            {
                var objects = (
                    from rltobj in context.RealtyObjects
                    join dstrct in context.Districts on rltobj.DistrictId equals dstrct.Id
                    where dstrct.Name == district && rltobj.NumberOfRooms == numberOfRooms
                    select rltobj.Price
                ).ToList().Aggregate((x, y) => x + y).ToString();

                decimal totalSum = Decimal.Parse(objects, System.Globalization.NumberStyles.Float);

                this.dataGridView1.Rows.Add(totalSum);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Columns.Clear();

            this.dataGridView1.Columns.Add("Min", "Минимальная стоимость проданного объекта недвижимости");
            this.dataGridView1.Columns.Add("Max", "Максимальная стоимость проданного объекта недвижимости");

            this.dataGridView1.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dataGridView1.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            string realtor = this.textBoxRealtor.Text;

            using (var context = new RealEstateAgencyContext())
            {
                var objects = (
                    from dl in context.Deals
                    join rlt in context.Realtors on dl.RealtorId equals rlt.Id
                    where rlt.Surname == realtor
                    select dl.Price
                ).ToList();

                decimal minPrice = Decimal.Parse(objects.Min().ToString(), System.Globalization.NumberStyles.Float);
                decimal maxPrice = Decimal.Parse(objects.Max().ToString(), System.Globalization.NumberStyles.Float);

                this.dataGridView1.Rows.Add(minPrice, maxPrice);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Columns.Clear();

            this.dataGridView1.Columns.Add("AverageScore", "Средняя оценка объектов недвижимости");

            this.dataGridView1.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            string district = this.textBoxDistrict2.Text;

            using (var context = new RealEstateAgencyContext())
            {
                var objects = (
                    from assmnt in context.RealtyObjectAssessments
                    from dstrct in context.Districts
                    join rltobj in context.RealtyObjects on assmnt.RealtyObjectId equals rltobj.Id
                    where rltobj.DistrictId == dstrct.Id
                        && dstrct.Name == district
                    select assmnt.Score
                ).ToList().Average().ToString();

                decimal averageScore = Decimal.Parse(objects, System.Globalization.NumberStyles.Float);

                this.dataGridView1.Rows.Add(averageScore);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Columns.Clear();

            this.dataGridView1.Columns.Add("Disctrict", "Район");
            this.dataGridView1.Columns.Add("NumberOfObjects", "Количество объектов недвижимости");

            this.dataGridView1.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dataGridView1.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            int floor = int.Parse(this.textBoxFloor1.Text);

            using (var context = new RealEstateAgencyContext())
            {
                var objects = (
                    from rltobj in context.RealtyObjects
                    join dstrct in context.Districts on rltobj.DistrictId equals dstrct.Id
                    where rltobj.Floor == floor
                    group rltobj by dstrct.Name
                ).ToList();

                foreach (var obj in objects)
                {
                    this.dataGridView1.Rows.Add(obj.Key, obj.Count());
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Columns.Clear();

            this.dataGridView1.Columns.Add("AverageScore", "Средняя оценка по выбранному критерию");

            this.dataGridView1.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            string realtyObjectType = this.textBoxRealtyObjectType.Text;
            string realtorSurname = this.textBoxRealtorSurname.Text;
            string criteriaType = this.textBoxCriteriaName.Text;

            using (var context = new RealEstateAgencyContext())
            {
                var avgPrice = (
                    from scr in context.RealtyObjectAssessments
                    from rltobj in context.RealtyObjects
                    from tp in context.RealtyObjectTypes
                    from dl in context.Deals
                    from crt in context.CriteriaTypes
                    from rltr in context.Realtors
                    where tp.Name == realtyObjectType
                        && rltobj.RealtyObjectTypeId == tp.Id
                        && scr.RealtyObjectId == rltobj.Id
                        && dl.RealtyObjectId == rltobj.Id
                        && crt.Name == criteriaType
                        && scr.CriteriaTypeId == crt.Id
                        && rltr.Surname == realtorSurname
                        && dl.RealtorId == rltr.Id
                    select scr.Score
                ).ToList().Average();

                this.dataGridView1.Rows.Add(avgPrice);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Columns.Clear();

            this.dataGridView1.Columns.Add("AveragePrice", "Cредняя продажная стоимость 1м2");

            this.dataGridView1.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            string realtyObjType = this.textBoxRealtyObjectType1.Text;
            var dateStart = DateTime.Parse(this.textBoxDateStart.Text);
            var dateEnd = DateTime.Parse(this.textBoxDateEnd.Text);

            using (var context = new RealEstateAgencyContext())
            {
                var avgPrice = (
                    from dl in context.Deals
                    join rltobj in context.RealtyObjects on dl.RealtyObjectId equals rltobj.Id
                    join tp in context.RealtyObjectTypes on rltobj.RealtyObjectTypeId equals tp.Id
                    where dl.DealDate >= dateStart && dl.DealDate <= dateEnd
                        && tp.Name == realtyObjType
                    select dl.Price / rltobj.Area
                ).ToList().Average();

                this.dataGridView1.Rows.Add(avgPrice);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Columns.Clear();

            this.dataGridView1.Columns.Add("Realtor", "ФИО риэлтора");
            this.dataGridView1.Columns.Add("Bonus", "Премия");

            this.dataGridView1.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dataGridView1.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            using (var context = new RealEstateAgencyContext())
            {
                var cc = from realtor in context.Realtors
                         join deal in context.Deals on realtor equals deal.Realtor into gj
                         from subdeal in gj.DefaultIfEmpty()
                         group subdeal by realtor;

                foreach (var r in cc)
                {
                    var realtor = r.Key.Surname + " " + r.Key.Name + " " + r.Key.Patronym;

                    if (r.Sum(p => p?.Price ?? 0) == 0)
                    {
                        this.dataGridView1.Rows.Add(realtor, "Ничего не продал");
                    }
                    else
                    {
                        this.dataGridView1.Rows.Add(realtor, r.Count() * r.Sum(p => p.Price) * 0.05 * 0.83);
                    }
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Columns.Clear();

            this.dataGridView1.Columns.Add("Realtor", "ФИО риэлтора");
            this.dataGridView1.Columns.Add("SaledObjects", "Количество проданных объектов недвижимости");

            this.dataGridView1.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dataGridView1.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            string RealtyObjectType = this.textBoxRealtyObjectType2.Text;

            using (var context = new RealEstateAgencyContext())
            {
                var cc = (
                    from realtor in context.Realtors
                    join deal in context.Deals on realtor equals deal.Realtor into realtordeal
                    from deal in realtordeal.DefaultIfEmpty()
                    group deal by realtor
                );

                foreach (var r in cc)
                {
                    var realtor = r.Key.Surname + " " + r.Key.Name + " " + r.Key.Patronym;

                    if (r.Sum(q => q?.Price ?? 0) == 0)
                    {
                        this.dataGridView1.Rows.Add(realtor, 0);
                    }
                    else
                    {
                        this.dataGridView1.Rows.Add(realtor, r.Count((q) =>
                        {
                            var obj = (
                                from rltobj in context.RealtyObjects
                                where rltobj.RealtyObjectType.Name == RealtyObjectType
                                    && rltobj.Id == q.RealtyObjectId
                                select rltobj
                            ).Count();

                            return obj > 0;
                        }));
                    }
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Columns.Clear();

            this.dataGridView1.Columns.Add("BuildingMaterial", "Материал здания");
            this.dataGridView1.Columns.Add("AveragePrice", "Cредняя стоимость объектов недвижимости");

            this.dataGridView1.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dataGridView1.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            int floor = int.Parse(this.textBoxFloor2.Text);

            using (var context = new RealEstateAgencyContext())
            {
                var objs = (
                    from mtral in context.BuildingMaterials
                    join rltobj in context.RealtyObjects on mtral equals rltobj.BuildingMaterial into mtralrltobj
                    from rltobj in mtralrltobj.DefaultIfEmpty()
                    group rltobj by mtral
                );

                foreach (var r in objs)
                {
                    var buildingMaterial = r.Key.Name;

                    if (r.Sum(p => p?.Price ?? 0) == 0)
                    {
                        this.dataGridView1.Rows.Add(buildingMaterial, 0);
                    }
                    else
                    {
                        this.dataGridView1.Rows.Add(buildingMaterial, Decimal.Parse(r.Where(q => q.Floor == floor).Select(q => q.Price).DefaultIfEmpty(0).Average().ToString(), System.Globalization.NumberStyles.Float));
                    }
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Columns.Clear();

            this.dataGridView1.Columns.Add("District", "Название района");
            this.dataGridView1.Columns.Add("Address", "Адрес");
            this.dataGridView1.Columns.Add("Price", "Стоимость");
            this.dataGridView1.Columns.Add("Floor", "Этаж");

            this.dataGridView1.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dataGridView1.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            using (var context = new RealEstateAgencyContext())
            {
                var obj = (
                    from rltobj in context.RealtyObjects
                    group rltobj by rltobj.District
                );

                foreach (var item in obj)
                {
                    var district = item.Key.Name;

                    foreach (var item1 in item.OrderByDescending(p => p.Price).ThenBy(p => p.Floor).Take(3))
                    {
                        this.dataGridView1.Rows.Add(district, item1.Address, Decimal.Parse(item1.Price.ToString(), System.Globalization.NumberStyles.Float), item1.Floor);
                    }
                }
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Columns.Clear();

            this.dataGridView1.Columns.Add("Address", "Адрес");

            this.dataGridView1.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            string district = this.textBoxDistrict3.Text;

            using (var context = new RealEstateAgencyContext())
            {
                var obj = (
                    from rltobj in context.RealtyObjects
                    where rltobj.Status == 1 && rltobj.District.Name == district
                    select rltobj.Address
                ).ToList();

                foreach (var item in obj)
                {
                    this.dataGridView1.Rows.Add(item);
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Columns.Clear();

            this.dataGridView1.Columns.Add("Address", "Адрес");
            this.dataGridView1.Columns.Add("District", "Название района");

            this.dataGridView1.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dataGridView1.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            string district = this.textBoxDistrict4.Text;
            float diff = float.Parse(this.textBoxDiff.Text);

            using (var context = new RealEstateAgencyContext())
            {
                var obj = (
                    from dl in context.Deals
                    where dl.RealtyObject.District.Name == district &&
                        (((dl.Price - dl.RealtyObject.Price) / dl.Price) * 100) <= diff
                    select new { dl.RealtyObject.Address, dl.RealtyObject.District.Name }
                ).ToList();

                foreach (var item in obj)
                {
                    this.dataGridView1.Rows.Add(item.Address, item.Name);
                }
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Columns.Clear();

            this.dataGridView1.Columns.Add("Address", "Адрес");
            this.dataGridView1.Columns.Add("District", "Название района");

            this.dataGridView1.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dataGridView1.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            string realtor = this.textBoxRealtor1.Text;
            float diff = float.Parse(this.textBoxDiff1.Text);

            using (var context = new RealEstateAgencyContext())
            {
                var obj = (
                    from dl in context.Deals
                    where dl.Realtor.Surname == realtor &&
                        (dl.Price - dl.RealtyObject.Price) > diff
                    select new { dl.RealtyObject.Address, dl.RealtyObject.District.Name }
                ).ToList();

                foreach (var item in obj)
                {
                    this.dataGridView1.Rows.Add(item.Address, item.Name);
                }
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Columns.Clear();

            this.dataGridView1.Columns.Add("Address", "Адрес");
            this.dataGridView1.Columns.Add("Diff", "Разница");

            this.dataGridView1.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dataGridView1.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            string realtor = this.textBoxRealtor2.Text;
            int year = int.Parse(this.textBoxYear.Text);

            using (var context = new RealEstateAgencyContext())
            {
                var obj = (
                    from dl in context.Deals
                    where dl.Realtor.Surname == realtor &&
                        dl.DealDate.Year == year
                    select new { dl.RealtyObject.Address, Diff = (((dl.Price - dl.RealtyObject.Price) / dl.Price) * 100) }
                ).ToList();

                foreach (var item in obj)
                {
                    this.dataGridView1.Rows.Add(item.Address, item.Diff);
                }
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Columns.Clear();

            this.dataGridView1.Columns.Add("Address", "Адрес");

            this.dataGridView1.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            using (var context = new RealEstateAgencyContext())
            {
                var avg_p = from rltobj in context.RealtyObjects
                            group rltobj by rltobj.District into g
                            select new { Dis = g.Key, Avg = g.Average(p => p.Price / p.Area) };

                var obj = from rltobj in context.RealtyObjects
                           from ap in avg_p
                           where rltobj.RealtyObjectType.Name == "Квартира"
                           where rltobj.District == ap.Dis && rltobj.Price / rltobj.Area < ap.Avg
                           select rltobj.Address;

                foreach (var item in obj)
                {
                    this.dataGridView1.Rows.Add(item);
                }
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Columns.Clear();

            this.dataGridView1.Columns.Add("Realtor", "ФИО риэтора");

            this.dataGridView1.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            using (var context = new RealEstateAgencyContext())
            {
                var obj = (
                    from realtor in context.Realtors
                    join deal in context.Deals on realtor equals deal.Realtor into realtordeal
                    from deal in realtordeal.DefaultIfEmpty()
                    group deal by realtor
                );

                foreach (var r in obj)
                {
                    var realtor = r.Key.Surname + " " + r.Key.Name + " " + r.Key.Patronym;

                    if (r.Sum(q => q?.Price ?? 0) == 0)
                    {
                        this.dataGridView1.Rows.Add(realtor);
                    }
                    else
                    {
                        var sales = r.Count((q) => q.DealDate.Year == DateTime.Now.Year);

                        if (sales == 0)
                        {
                            this.dataGridView1.Rows.Add(realtor);
                        }
                    }
                }
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Columns.Clear();

            this.dataGridView1.Columns.Add("Address", "Адрес");
            this.dataGridView1.Columns.Add("Status", "Статус");

            this.dataGridView1.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dataGridView1.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            using (var context = new RealEstateAgencyContext())
            {
                var avg_pr = from rltobj in context.RealtyObjects
                            group rltobj by rltobj.District into g
                            select new { District = g.Key, Avg = g.Average(p => p.Price / p.Area) };

                var obj = from rltobj in context.RealtyObjects
                          from ap in avg_pr
                          where ((DateTime.Now.Year - rltobj.AnnouncementDate.Year) * 12) + DateTime.Now.Month - rltobj.AnnouncementDate.Month <= 4
                          where rltobj.District == ap.District && rltobj.Price / rltobj.Area < ap.Avg
                          select new { rltobj.Address, Status = rltobj.Status == 1 ? "В продаже" : "Продано" };

                foreach (var r in obj)
                {
                    this.dataGridView1.Rows.Add(r.Address, r.Status);
                }
            }
        }
    }
}
