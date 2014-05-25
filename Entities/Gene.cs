namespace Entities
{
  public class Gene
  {
    int Type{ get; set; }
    int Position{ get; set; }

      public Gene (int type, int position)
      {
          Type = type;
          Position = position;
      }

    /* Символы для трубы
     * { "0070", "2014", "2308", "2309", "230A", "230B" };
     */
  }
}
