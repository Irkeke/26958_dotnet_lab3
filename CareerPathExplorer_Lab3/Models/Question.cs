namespace CareerPathExplorer.Models;
public class Question { public int Id {get;set;} public string Text {get;set;}=""; public List<Option> Options {get;set;}=new(); }
public class Option { public int Id {get;set;} public string Text {get;set;}=""; public string Area {get;set;}=""; }