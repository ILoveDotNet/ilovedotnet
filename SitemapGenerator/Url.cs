internal class Url
{
  public string Loc { get; set; } = string.Empty;
  public DateTime LastMod { get; set; }
  public string ChangeFreq { get; set; } = "weekly";
  public double Priority { get; set; } = 0.5;
}
