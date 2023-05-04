using System;

[Serializable]

public class Scoredata
{
public int Score;
public string Time;

public Scoredata(int Score,string Time){
    this.Score= Score;
    this.Time = Time;
}
}