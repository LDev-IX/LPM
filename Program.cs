using System;
using System.IO;
using System.Net.Http;
using System.Diagnostics;
using System.Collections.Generic;

static class Program{
    public static string path_lpm = @"C:\ProgramData\LPM";
    public static string path_lpm_packages = @$"{path_lpm}\packages";

    static void Main(){
        // Pre-init
        Console.WriteLine("LPM>> Starting");
        if(!Directory.Exists(path_lpm)){
            Directory.CreateDirectory(path_lpm);
            Directory.CreateDirectory(path_lpm_packages);
        }else{

        }

    }

    static void Unzip(string arg_source_path, string arg_target_path){
        ProcessStartInfo psi = new();
        psi.FileName = @$"{Environment.CurrentDirectory}\exts\7za.exe";
        psi.Arguments = $"x -o{arg_target_path} {arg_source_path}";
        psi.CreateNoWindow = true;
        Process zip = new();
        zip.StartInfo = psi;
        zip.Start();
    }

    static void Download(string arg_link, string arg_target_path){
        HttpClient hc = new();
        byte[] hc_data = hc.GetByteArrayAsync(arg_link).Result;
        File.WriteAllBytes(arg_target_path, hc_data);
    }
}

public static class AppList{
    public static List<App> apps {get; set;}

    public static void Load(string arg_source_path){
        foreach(string line in File.ReadAllLines(arg_source_path)){
            App app = new(arg_source_path);
            apps.Add(app);
        }
    }
}

public class App{
    public string name {get; set;}
    public string desc {get; set;}
    public string auth {get; set;}
    public string ver {get; set;}
    public string link {get; set;}
    public bool is_downloaded {get; set;}

    public App(string line){
        string[] line_split = line.Replace(" | ", "|").Split('|', 5);
        name = line_split[0];
        desc = line_split[1];
        auth = line_split[2];
        ver = line_split[3];
        link = line_split[4];
        if(Directory.Exists(@$"{Program.path_lpm_packages}\{name}")){
            is_downloaded = true;
        }else{
            is_downloaded = false;
        }
    }
}