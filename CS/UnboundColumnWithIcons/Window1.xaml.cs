﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Grid;
using System.Collections.ObjectModel;

namespace UnboundColumnWithIcons {
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window {
        ObservableCollection<MyObject> dataSource;
		public Window1() {
			InitializeComponent();

            dataSource = new ObservableCollection<MyObject>();
            dataSource.Add(new MyObject("cut"));
            dataSource.Add(new MyObject("copy"));
            dataSource.Add(new MyObject("paste"));
            dataSource.Add(new MyObject("delete"));
			grid.DataSource = dataSource;
		}

		private void GridControl_CustomUnboundColumnData(object sender, GridColumnDataEventArgs e) {
            if(e.Column.FieldName == "IconUnbound") {
                if(e.IsGetData) {
                    MyObject row = dataSource[e.ListSourceRowIndex];
                    string resourceName = GetResourceName(row.Action);
                    e.Value = GetImage(resourceName);
                }
            }
		}
		BitmapFrame GetImage(string resourceName) {
			Uri uri = new Uri("pack://application:,,,/Icons/" + resourceName, UriKind.Absolute);			
			return BitmapFrame.Create(uri);
		}
		string GetResourceName(string action) {
            switch(action) {
				case "copy":
					return "copy32x32.png";
				case "cut":
					return "cut32x32.png";
				case "delete":
					return "delete32x32.png";
				case "paste":
					return "paste32x32.png";
			}
			return string.Empty;
		}
	}
	public class MyObject {
        public MyObject() { }
        public MyObject(string action) {
			Action = action;
		}
		public string Action { get; set; }
	}
}