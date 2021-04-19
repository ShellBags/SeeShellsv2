﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using SeeShellsV2.Data;
using SeeShellsV2.Repositories;
using SeeShellsV2.Services;
using System.Collections;
using System.Threading;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Documents;

namespace SeeShellsV2.UI
{
	public class ExportWindowVM : ViewModel, IExportWindowVM
	{
		[Dependency]
		public PdfExporter Exporter { get; set; }

		public ObservableCollection<IPdfModule> moduleList { get; set; }

		public ObservableCollection<string> moduleSelector { get; set; }

		public string Status
		{
			get => _status;
			set { _status = value; NotifyPropertyChanged(); }
		}

		private string _status = string.Empty;

		public ExportWindowVM([Dependency] PdfExporter Export) 
		{
			moduleList = new ObservableCollection<IPdfModule>();
			moduleList.Add(Export.moduleNames["TextBox"].Clone());
			moduleList.Add(Export.moduleNames["HeatMap"].Clone());

			moduleSelector = new ObservableCollection<string>(Export.moduleNames.Keys);
			moduleSelector.Insert(0, "Select Module");

			Status = "Save";
		}

		public void Export_PDF(string filename)
		{
			Exporter.Export(filename, moduleList);
			//Status = "Saving...";
			//await Task.Run(() => Exporter.Export(filename, moduleList));
			//Status = "Saved";
			//await Task.Run(() => Thread.Sleep(5000));
			//Status = "Save";
		}

		public void Remove(IPdfModule pdfModule)
		{
			moduleList.Remove(pdfModule);
		}

		public void MoveDown(IPdfModule pdfModule)
		{
			int pos = moduleList.IndexOf(pdfModule);
			if (pos < moduleList.Count - 1)
				moduleList.Move(pos, pos + 1);
		}

		public void MoveUp(IPdfModule pdfModule)
		{
			int pos = moduleList.IndexOf(pdfModule);
			if (pos >= 1)
				moduleList.Move(pos, pos - 1);
		}

		public void AddModule(string module)
		{
			moduleList.Add(Exporter.moduleNames[module].Clone());
		}
	}
}