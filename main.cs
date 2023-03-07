using Godot;
using System;
using System.Collections.Generic;

public partial class main : Node2D
{
	public List<Package> glob_packages = new();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Get packages
		System.Net.Http.HttpClient init_hc = new();
		string init_result_raw = init_hc.GetStringAsync("https://raw.githubusercontent.com/LDev-IX/LPM/main/.list").Result;
		string[] init_result = init_result_raw.Split("\n", 64);
		foreach(string fline in init_result){
			string temp_fline = fline.Replace(" | ", "|");
			string[] temp_params = temp_fline.Split('|', 5);
			if(temp_params.Length < 5) break;
			Package temp_package = new(temp_params[0], temp_params[1], temp_params[2], temp_params[3], temp_params[4]);
			glob_packages.Add(temp_package);
		}

		// Display packages
		ItemList node_itemlist = GetNode<ItemList>("MarginContainer/VBoxContainer/VBoxContainer/ScrollContainer/ItemList");
		foreach(Package fpackage in glob_packages){
			node_itemlist.AddItem(fpackage.name);
		}
	}

	private void _OnItemListSelect(int index){
		Label node_labeldescription = GetNode<Label>("MarginContainer/VBoxContainer/VBoxContainer2/Label2");
		node_labeldescription.Text = glob_packages[index].description;
		GD.Print(glob_packages[index].name);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

public class Package{
	public string name {get; set;}
	public string description {get; set;}
	public string author {get; set;}
	public string website {get; set;}
	public string download {get; set;}

	public Package(string aname, string adescription, string aauthor, string awebsite, string adownload){
		name = aname;
		description = adescription;
		author = aauthor;
		website = awebsite;
		download = adownload;
	}
}
