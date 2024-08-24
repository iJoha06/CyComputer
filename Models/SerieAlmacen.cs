namespace CyComputer.Models
{
    public class SerieAlmacen
    {
        //name: 'Chrome',
        //        y: 74.77,
        //        sliced: true,
        //        selected: true

        public string name { get; set; }
        public double y { get; set; }
        public bool sliced { get; set; }
        public bool selected { get; set; }

        public SerieAlmacen()
        {
            
        }
        public SerieAlmacen(string name, double y, bool sliced=false, bool selected=false)
        {
            this.name = name;
            this.y = y;
            this.sliced = sliced;
            this.selected = selected;
        }

        public List<SerieAlmacen> GetDataDummy()
        {
            List<SerieAlmacen> lista = new List<SerieAlmacen>();

            lista.Add(new SerieAlmacen("Memoria RAM", 45));
            lista.Add(new SerieAlmacen("Procesador AMD", 50));
            lista.Add(new SerieAlmacen("Monitor LG 24pulg", 60));
            lista.Add(new SerieAlmacen("Mouse gamer", 34));
            lista.Add(new SerieAlmacen("Fuente de poder", 20));
            lista.Add(new SerieAlmacen("Tarjeta SSD", 40));
            lista.Add(new SerieAlmacen("Tarjeta HDD", 10));
            lista.Add(new SerieAlmacen("Microfono HyperX", 50));

            return lista;
        }
    }
}
