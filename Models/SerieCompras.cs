namespace CyComputer.Models
{
    public class SerieCompras
    {
        //name: 'Chrome',
        //        y: 74.77,
        //        sliced: true,
        //        selected: true

        public string name { get; set; }
        public double y { get; set; }
        public bool sliced { get; set; }
        public bool selected { get; set; }

        public SerieCompras()
        {

        }
        public SerieCompras(string name, double y, bool sliced = false, bool selected = false)
        {
            this.name = name;
            this.y = y;
            this.sliced = sliced;
            this.selected = selected;
        }

        public List<SerieCompras> GetDataDummy()
        {
            List<SerieCompras> lista = new List<SerieCompras>();

            lista.Add(new SerieCompras("Memoria RAM", 45));
            lista.Add(new SerieCompras("Procesador AMD", 50));
            lista.Add(new SerieCompras("Monitor LG 24pulg", 60));
            lista.Add(new SerieCompras("Mouse gamer", 12));
            lista.Add(new SerieCompras("Fuente de poder", 20));
            lista.Add(new SerieCompras("Tarjeta SSD", 40));
            lista.Add(new SerieCompras("Tarjeta HDD", 10));
            lista.Add(new SerieCompras("Microfono HyperX", 5));

            return lista;
        }
    }
}
