using FusionCharts.DataEngine;
using FusionCharts.Visualization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace TestWebApplicationHTTP.Pages
{
    public class Chart1Model : PageModel
    {
        // create a public property. OnGet method() set the chart configuration json in this property.
        // When the page is being loaded, OnGet method will be  invoked
        public string ChartJson { get; internal set; }

        public string ChartJson2 { get; internal set; }

        [BindProperty]
        public DateOnly StartDate { get; set; }

        [BindProperty]
        public DateOnly EndDate { get; set; }

        public void OnGet()
        {
            
        }

        public IActionResult OnPostShowCharts()
        {
            if (EndDate < StartDate)
                return Page();

            // create data table to store data
            DataTable ChartData = new DataTable();
            // Add columns to data table
            ChartData.Columns.Add("Дата", typeof(string));
            ChartData.Columns.Add("Время", typeof(double));
            // Add rows to data table

            Random random = new Random();

            Dictionary<DateOnly, int> AvgParkingTime = new Dictionary<DateOnly, int>();
            for (DateOnly i = StartDate; i <= EndDate; i.AddDays(1))
            {
                ChartData.Rows.Add(i.ToShortDateString(), random.Next(650, 920));
                i = i.AddDays(1);
            }

            // Create static source with this data table
            StaticSource source = new StaticSource(ChartData);
            // Create instance of DataModel class
            DataModel model = new DataModel();
            // Add DataSource to the DataModel
            model.DataSources.Add(source);
            // Instantiate Column Chart
            Charts.ColumnChart column = new Charts.ColumnChart("first_chart");
            // Set Chart's width and height
            column.Width.Pixel(700);
            column.Height.Pixel(400);
            // Set DataModel instance as the data source of the chart
            column.Data.Source = model;
            // Set Chart Title
            column.Caption.Text = "Среднее время нахождения на парковке";
            // Set chart sub title
            column.SubCaption.Text = StartDate.ToShortDateString() + " - " + EndDate.ToShortDateString();
            // hide chart Legend
            column.Legend.Show = false;
            // set XAxis Text
            column.XAxis.Text = "Дата";
            // Set YAxis title
            column.YAxis.Text = "Время";
            // set chart theme
            column.ThemeName = FusionChartsTheme.ThemeName.FUSION;
            // set chart rendering json
            ChartJson = column.Render();


            // create data table to store data
            DataTable ChartData2 = new DataTable();
            // Add columns to data table
            ChartData2.Columns.Add("Дата", typeof(string));
            ChartData2.Columns.Add("Время", typeof(double));
            // Add rows to data table

            for (DateOnly i = StartDate; i <= EndDate; i.AddDays(1))
            {
                ChartData2.Rows.Add(i.ToShortDateString(), random.Next(115, 180));
                i = i.AddDays(1);
            }

            // Create static source with this data table
            StaticSource source2 = new StaticSource(ChartData2);
            // Create instance of DataModel class
            DataModel model2 = new DataModel();
            // Add DataSource to the DataModel
            model2.DataSources.Add(source2);
            // Instantiate Column Chart
            Charts.ColumnChart column2 = new Charts.ColumnChart("second_chart");
            // Set Chart's width and height
            column2.Width.Pixel(700);
            column2.Height.Pixel(400);
            // Set DataModel instance as the data source of the chart
            column2.Data.Source = model2;
            // Set Chart Title
            column2.Caption.Text = "Количество перемещений";
            // Set chart sub title
            column2.SubCaption.Text = StartDate.ToShortDateString() + " - " + EndDate.ToShortDateString();
            // hide chart Legend
            column2.Legend.Show = false;
            // set XAxis Text
            column2.XAxis.Text = "Дата";
            // Set YAxis title
            column2.YAxis.Text = "Количество";
            // set chart theme
            column2.ThemeName = FusionChartsTheme.ThemeName.FUSION;
            // set chart rendering json
            ChartJson2 = column2.Render();

            return Page();
        }
    }
}
