﻿using SeeShellsV2.Repositories;
using SeeShellsV2.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Xml;
using Unity;

namespace SeeShellsV2.Services
{
	class CaptionedHeatMapModule : IPdfModule
	{
		public string Name => "Captioned HeatMap";

		public FrameworkElement HeatMap { get; set; }
		public FrameworkElement TextBox { get; set; }

		[Dependency]
		public IShellEventCollection ShellEvents { get; set; }


		public ICollectionView FilteredShellEvents => ShellEvents.FilteredView;

		public CaptionedHeatMapModule([Dependency] IShellEventCollection shellEvents)
		{
			ShellEvents = shellEvents;
		}

		public IPdfModule Clone()
		{
			return MemberwiseClone() as IPdfModule;
		}

		public UIElement Render()
		{
			if (TextBox == null || HeatMap == null)
				return null;

			//string s = XamlWriter.Save(Rtb);
			//StringReader sr = new StringReader(s);
			//XmlReader reader = XmlTextReader.Create(sr, new XmlReaderSettings());
			//FrameworkElement e = (FrameworkElement)XamlReader.Load(reader);
			//var rtb = e.FindName("RichTextBox") as RichTextBox;

			TextBlock caption = new TextBlock();
			caption.TextWrapping = TextWrapping.Wrap;
			caption.TextAlignment = TextAlignment.Left;
			caption.Width = 500;
			caption.Height = 800;
			caption.Text = (TextBox as TextBox).Text;

			var plot = (HeatMap as CalendarHeatMap).HeatMapPlot;
			var bmp = plot.ToBitmap();
			Image image = new Image();
			image.Source = bmp;
			image.Width = bmp.Width;
			image.Height = bmp.Height;

			StackPanel sp = new StackPanel();
			sp.Width = image.Width;
			TextBlock t = new TextBlock();
			t.Text = (HeatMap as CalendarHeatMap).Year.ToString();
			t.FontSize = plot.TitleFontSize;
			t.FontWeight = plot.TitleFontWeight;
			t.FontFamily = plot.FontFamily;
			t.HorizontalAlignment = HorizontalAlignment.Center;
			sp.Children.Add(t);
			sp.Children.Add(image);

			StackPanel captioned = new StackPanel();
			captioned.Orientation = Orientation.Horizontal;
			captioned.Children.Add(sp);
			captioned.Children.Add(caption);
			

			//Figure fig = new Figure();
			//fig.HorizontalAnchor = FigureHorizontalAnchor.PageLeft;
			////FigureLength fl = new FigureLength(0.3, FigureUnitType.Column);
			////fig.Width = fl;
			//fig.Blocks.Add(bc);

			//Paragraph p = new Paragraph();
			//p.Inlines.Add(fig);

			////Floater fig = new Floater();
			////fig.HorizontalAlignment = HorizontalAlignment.Left;
			////fig.Width = image.Width;
			////fig.Blocks.Add(bc);

			//List<Block> rtbBlocks = new List<Block>(rtb.Document.Blocks);

			//foreach (Block block in rtbBlocks)
			//{
			//	Debug.WriteLine(block.GetType().Name);
			//}

			//foreach (Block block in rtbBlocks)
			//{
			//	List<Inline> inlines = new List<Inline>((block as Paragraph).Inlines);
			//	p.Inlines.AddRange(inlines);
			//}

			//FlowDocument fd = new FlowDocument(p);
			//RichTextBox r = new RichTextBox(fd);

			return captioned as UIElement;
		}

		public FrameworkElement View()
		{

			// the view we will display for the user to configure this object
			string view = @"
			<StackPanel Orientation=""Horizontal"" MinHeight=""800"">
				<local:CalendarHeatMap x:Name=""Heatmap""
					Background=""White""
					ColorAxisTitle =""User Action Frequency""
					ItemsSource =""{Binding FilteredShellEvents}""
					SelectionBegin=""{Binding DateSelectionBegin}""
					SelectionEnd=""{Binding DateSelectionEnd}""
					SelectionColor=""{ DynamicResource MahApps.Colors.Accent}""
					DateTimeProperty=""TimeStamp"" Orientation=""Vertical"" />
				<StackPanel>
				<ToolBar>
					<Button Command=""ApplicationCommands.Cut"" ToolTip=""Cut"" FontFamily=""Segoe MDL2 Assets"" Content=""&#xE8C6;""/>
					<Button Command=""ApplicationCommands.Copy"" ToolTip= ""Copy"" FontFamily=""Segoe MDL2 Assets"" Content=""&#xE8C8;""/>
					<Button Command=""ApplicationCommands.Paste"" ToolTip= ""Paste"" FontFamily=""Segoe MDL2 Assets"" Content=""&#xE77F;""/>	   
				</ToolBar>
				<TextBox Name=""TextBox"" 
					TextAlignment=""Left""
					TextWrapping=""Wrap"" 
					BorderBrush=""Transparent"" CaretBrush=""Black"" 
					Foreground=""Black"" Background=""White"" 
					AcceptsTab=""True"" AcceptsReturn=""True""
					Height=""575"" Width=""450"" 
					MaxHeight=""575"" MaxLength=""2692""/>
				</StackPanel>
			</StackPanel>";


			//// add WPF namespaces to a parser context so we can parse WPF tags like StackPanel
			ParserContext context = new ParserContext();
			context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
			context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");
			context.XmlnsDictionary.Add("d", "http://schemas.microsoft.com/expression/blend/2008");
			context.XmlnsDictionary.Add("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
			context.XmlnsDictionary.Add("oxy", "http://oxyplot.org/wpf");
			context.XmlnsDictionary.Add("mah", "http://metro.mahapps.com/winfx/xaml/controls");

			CalendarHeatMap heat = new CalendarHeatMap();
			Type type = heat.GetType();
			context.XamlTypeMapper = new XamlTypeMapper(new string[0]);
			context.XamlTypeMapper.AddMappingProcessingInstruction("local", type.Namespace, type.Assembly.FullName);
			context.XmlnsDictionary.Add("local", "local");

			//// construct the view using an XAML parser
			FrameworkElement e = XamlReader.Parse(view, context) as FrameworkElement;

			//// assign this object as the data context so the view can get/set module properties
			e.DataContext = this;

			//// extract the elements from the view. this is only necessary if we
			//// want to use these elements *directly* to do work inside this class.
			//// **any data access that can be done with xaml bindings should be done with xaml bindings**
			var hm = e.FindName("Heatmap") as CalendarHeatMap;
			var tb = e.FindName("TextBox") as TextBox;

			//// hook up event listeners

			//// save RTB element for later
			HeatMap = hm;
			TextBox = tb;

			return e;
		}
	}
}
