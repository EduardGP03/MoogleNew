namespace MoogleEngine;
//esta clase es para almacenar toda la informacion relevante de un archivo
public class Book
{
    //direccion de el archivo que se esta leyendo
    string Location { get; set; }
    //titulo del archivo que se esta leyendo
    public string Title { get; private set; }
    //diccionario donde se almacena el tf de cada palabra en el archivo
    Dictionary<string,float> TFS { get; set;}
    //almacena el maximo tf del libro
    float MaxTF { get; set; }
    //constructor de la clase
    public Book(string Location)
    {
        //inicializamos el diccionario
        TFS = new Dictionary<string, float>();
        MaxTF = 0f;
        //copiamos la ruta de acceso del archivo
        this.Location = Location;
        //extraemos el titulo
        Title = Location.Substring(Location.LastIndexOf("/") + 1);
        Title = Title.Substring(0,Title.Length - 4);
        //leemos el contenido
        StreamReader reader = new StreamReader(Location);
        string text = reader.ReadToEnd().ToLower();
        reader.Close();
        //limpiamos el contenido de las expresiones regulares
        string[] words = text.Split(' ',',','.',';',':','\n','\t');
        //limpiamos los espacios en blanco
        words = Tools.ClearWhiteSpaces(words);
        //computamos el tf de cada palabra
        foreach(var word in words)
        {
            if(!TFS.Keys.Contains(word))
                TFS[word] = 1;
            else
                TFS[word]++;
            if(TFS[word] > MaxTF)
                MaxTF = TFS[word];
        }
    }
    public string[] Words
    {
        get
        {
            return TFS.Keys.ToArray();
        }
    }
    public float TF(string word)
    {
        return TFS.Keys.Contains(word) ? TFS[word]/MaxTF : 0f;
    }
    //devuelve un fragmento del texto del archivo en cuestion
    public string GetFragment(string[] words)
    {
        StreamReader reader = new StreamReader(this.Location);
        string content = reader.ReadToEnd();
        string copy = content.ToLower();
        foreach(var word in words)
        {
            if(copy.Contains(word))
            {
                //si contiene alguna de las palabras
                //devolvemos un fragmento con a lo sumo 400 caracteres comenzando por la palabra que se haya encontrado
                int index = copy.IndexOf(word);
                return content.Substring(index, Math.Min(400,content.Length - index));
            }
        }
        //devolvemos un fragmento desde el inicio  del archivo con a lo sumo 400 caracteres
        return content.Substring(0,Math.Min(400,content.Length));
    }
}