using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using EventClasses;
using ToolsCSharp;

using Business = EventClasses.Event;

namespace ToolsUI
{
    public partial class EventGridForm : Form
    {
        private string cnString;
        //private List<Event> events;
        private List<Business> list;
        Dictionary<string, SortOrder> sortOrders;
        string lastSort = "ID";

        public EventGridForm(string dbConnectionString)
        {
            InitializeComponent();

            cnString = dbConnectionString;
            SetupSort();
            RefreshGrid(lastSort, true);
        }

        // this will have to be abstract
        private void SetupSort()
        {
            sortOrders = new Dictionary<string, SortOrder>();
            sortOrders.Add("ID", SortOrder.Ascending);
            sortOrders.Add("UserID", SortOrder.Ascending);
            sortOrders.Add("Date", SortOrder.Ascending);
            sortOrders.Add("Title", SortOrder.Ascending);
            sortOrders.Add("Description", SortOrder.Ascending);
        }

        private void RefreshGrid(string propertyName, bool changeSort)
        { 
            gridView.AutoGenerateColumns = true;
            list = Business.GetList(cnString);

            list = Sort(list, propertyName, changeSort);
            gridView.DataSource = list;

            gridView.Columns["IsNew"].Visible = false;
            gridView.Columns["IsDirty"].Visible = false;
            gridView.Columns["IsValid"].Visible = false;
            gridView.Columns["IsDeleted"].Visible = false;
        }

        // this will have to be abstract
        private List<Business> Sort(List<Business> list, string propertyName, bool changeSort)
        {
            SortOrder sortOrder;

            lastSort = propertyName;

            if (changeSort)
                sortOrder = sortOrders[propertyName];
            else
            {
                if (sortOrders[propertyName] == SortOrder.Ascending)
                    sortOrder = SortOrder.Descending;
                else
                    sortOrder = SortOrder.Ascending;
            }

            switch (propertyName)
            {
                case "ID":
                    if (sortOrder == SortOrder.Ascending)
                    {
                        list = list.OrderBy(o => o.ID).ToList();
                        sortOrders[propertyName] = SortOrder.Descending;
                    }
                    else
                    {
                        list = list.OrderByDescending(o => o.ID).ToList();
                        sortOrders[propertyName] = SortOrder.Ascending;
                    }
                    break;
                case "UserID":
                    if (sortOrder == SortOrder.Ascending)
                    {
                        list = list.OrderBy(o => o.UserID).ToList();
                        sortOrders[propertyName] = SortOrder.Descending;
                    }
                    else
                    {
                        list = list.OrderByDescending(o => o.UserID).ToList();
                        sortOrders[propertyName] = SortOrder.Ascending;
                    }
                    break;
                case "Title":
                    if (sortOrder == SortOrder.Ascending)
                    {
                        list = list.OrderBy(o => o.Title).ToList();
                        sortOrders[propertyName] = SortOrder.Descending;
                    }
                    else
                    {
                        list = list.OrderByDescending(o => o.Title).ToList();
                        sortOrders[propertyName] = SortOrder.Ascending;
                    }
                    break;
                case "Description":
                    if (sortOrder == SortOrder.Ascending)
                    {
                        list = list.OrderBy(o => o.Description).ToList();
                        sortOrders[propertyName] = SortOrder.Descending;
                    }
                    else
                    {
                        list = list.OrderByDescending(o => o.Description).ToList();
                        sortOrders[propertyName] = SortOrder.Ascending;
                    }
                    break;
                case "Date":
                    if (sortOrder == SortOrder.Ascending)
                    {
                        list = list.OrderBy(o => o.Date).ToList();
                        sortOrders[propertyName] = SortOrder.Descending;
                    }
                    else
                    {
                        list = list.OrderByDescending(o => o.Date).ToList();
                        sortOrders[propertyName] = SortOrder.Ascending;
                    }
                    break;
                default:
                    if (sortOrder == SortOrder.Ascending)
                    {
                        list = list.OrderBy(o => o.ID).ToList();
                        sortOrders[propertyName] = SortOrder.Descending;
                    }
                    else
                    {
                        list = list.OrderByDescending(o => o.ID).ToList();
                        sortOrders[propertyName] = SortOrder.Ascending;
                    }
                    break;

            }
            return list;
        }

        // this will have to be abstract
        private object GetPrimaryKey(int rowIndex)
        {
            return list[rowIndex].ID;
        }

        // this will have to be abstract
        private void DeleteObject(object key)
        {
            Business.Delete((int)key, cnString);
        }

        private void gridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string columnName = gridView.Columns[e.ColumnIndex].DataPropertyName;
            list = Sort(list, columnName, true);
            gridView.DataSource = list;
        }

        private void gridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // this will have to change
            if (e.RowIndex != -1)
            {
                object key = GetPrimaryKey(e.RowIndex);
                DeleteObject(key);
                RefreshGrid(lastSort, false);
            }
        }
    }
}
