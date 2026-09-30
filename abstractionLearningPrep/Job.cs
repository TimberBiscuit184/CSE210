using System;

public class Job
{
    public string _jobTitle;
    public string _company;
    public double _startYear;
    public double _endYear;
    public void Display()
    {
     Console.WriteLine($"{_jobTitle} ({_company}) {_startYear}-{_endYear}");
    }
}