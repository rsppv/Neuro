using System;

namespace Entities.GA
{
  public class Gene
  {
    public int Type { get; set; }
    public int Position{ get; set; }

      public Gene (int type, int position)
      {
          Type = type;
          Position = position;
      }
      public void Mutate()
      {
          throw new NotImplementedException();
      }

    /* Символы для трубы
     * { "0070", "2014", "2308", "2309", "230A", "230B" };
     */
  }
}
