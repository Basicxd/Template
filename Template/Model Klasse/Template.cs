namespace Template.Model
{
    public class Template
    {
        //Det her er en standard Model Klasse som er nødvendig når man laver REST

        // Her er mine Propertis
        public int Id { get; set; }
        public string Navn { get; set; }


        //Vi opretter denne constructor for at lave obejcter til vores static klasse i RESTen  
        //Dem der er i () hedder Arguments
        public Template(int id, string navn)
        {
            
            Id = id;
            Navn = navn;
        }

        // Tøm Constructer 
        public Template()
        {
        }
    }
}
