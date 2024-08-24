namespace CyComputer.Models
{
    public class SerieVentas
    {
        //name: 'Chrome',
        //        y: 74.77,
        //        sliced: true,
        //        selected: true

        public string name { get; set; }
        public double y { get; set; }
        public bool sliced { get; set; }
        public bool selected { get; set; }

        public SerieVentas()
        {

        }
        public SerieVentas(string name, double y, bool sliced = false, bool selected = false)
        {
            this.name = name;
            this.y = y;
            this.sliced = sliced;
            this.selected = selected;
        }

        public List<SerieVentas> GetDataDummy()
        {
            List<SerieVentas> lista = new List<SerieVentas>();

            lista.Add(new SerieVentas("Memoria RAM", 65));
            lista.Add(new SerieVentas("Procesador AMD", 50));
            lista.Add(new SerieVentas("Monitor LG 24pulg", 60));
            lista.Add(new SerieVentas("Mouse gamer", 84));

            return lista;
        }
    }
}
