// See https://aka.ms/new-console-template for more information

using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.CompilerServices;

public class Principal
{
    public static void Main(string[] args)
    {
        try
        {
            Editor editor1 = new Editor();
            var guardado = editor1.Creacion();
            var mensaje = editor1.Aniadir(guardado);
            Console.WriteLine(mensaje);
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Puntos o figura fuera de rango.");
        }catch (FormatException)
        {
            Console.WriteLine("Tipo de dato inválido.");
        }

    }
}
class Editor
{
    private List<IGrafico> editores = new List<IGrafico>();

    public GraficoCompuesto Creacion()
    {
        Punto punto;
        GraficoCompuesto compuesto = new GraficoCompuesto();
        Console.WriteLine("Bienvenido a su editor\nEscriba los puntos de coordenada\nEje x: ");
        int x = int.Parse(Console.ReadLine());
        Console.WriteLine("Eje y: ");
        int y = int.Parse(Console.ReadLine());
        punto = new Punto(x, y);
        Console.WriteLine("Escriba el valor del radio: ");
        int radio = int.Parse(Console.ReadLine());
        Circulo circulo = new Circulo(x, y, radio);
        Console.WriteLine("Escriba el valor de la altura: ");
        int altura = int.Parse(Console.ReadLine());
        Console.WriteLine("Escriba el valor del ancho: ");
        int ancho = int.Parse(Console.ReadLine());
        Rectangulo rectangulo = new Rectangulo(x, y, altura, ancho);
        Console.WriteLine("Menu: \n1 Mover gráfico.\n2 Salir");
        String eleccion = Console.ReadLine();
        compuesto.Aniadir(punto);
        compuesto.Aniadir(circulo);
        compuesto.Aniadir(rectangulo);
        Menu(eleccion, circulo, rectangulo);
        return compuesto;
    }

    private void Menu(String eleccion, Circulo circulo, Rectangulo rectangulo)
    {
        while (eleccion == "1")
        {
            Console.WriteLine("¿Qué gráfico desea mover?\n1 Circulo.\n2 Rectángulo.");
            String respuesta = Console.ReadLine();
            switch (respuesta)
            {
                case "1":
                    Console.WriteLine(circulo.Dibujar());
                    Console.WriteLine("Escriba el nuevo valor de la coordenada x: ");
                    int xCirulo = int.Parse(Console.ReadLine());
                    Console.WriteLine("Escriba el nuevo valor de la coordenada y: ");
                    int yCirulo = int.Parse(Console.ReadLine());
                    circulo.Mover(xCirulo, yCirulo);
                    Console.WriteLine(circulo.Dibujar());
                    break;
                case "2":
                    Console.WriteLine(rectangulo.Dibujar());
                    Console.WriteLine("Escriba el nuevo valor de la coordenada x: ");
                    int xRectangulo = int.Parse(Console.ReadLine());
                    Console.WriteLine("Escriba el nuevo valor de la coordenada y: ");
                    int yRectangulo = int.Parse(Console.ReadLine());
                    rectangulo.Mover(xRectangulo, yRectangulo);
                    Console.WriteLine(rectangulo.Dibujar());
                    break;
            }
            Console.WriteLine("Menu: \n1 Mover gráfico.\n2 Salir");
            eleccion = Console.ReadLine();
        }
        Console.WriteLine("Fin del programa de movimiento de gráfico.");
    }

    public String Aniadir(IGrafico grafico)
    {
        editores.Add(grafico);
        return "Editor añadido correctamente.";
    }
}

public interface IGrafico
{

    Boolean Mover(int x, int y);
    String Dibujar();
}

public class GraficoCompuesto: IGrafico
{
    private List<IGrafico> graficos = new List<IGrafico>();   


    public Boolean Mover(int x, int y)
    {
        var retorno = true;
        for(int i = 0;  i < graficos.Count; i++)
        {
            if(graficos[i].Mover(x, y))
            {
                retorno =  true;
            }
            else
            {
                retorno = false;
            }
        }
        return retorno;
    }

    public String Dibujar()
    {
        String resultado = "";
        for(int i = 0; i < graficos.Count; i++){
            resultado += graficos[i].Dibujar() + "\n";
        }
        return resultado;
    }

    public void Aniadir(IGrafico grafico)
    {
        graficos.Add(grafico);
    }
}

public class Punto: IGrafico
{
    protected int x;

    public int X
    { get { return x; }
        set { if (value >= 0 && value <= 800)
            {
                x = value;
            }
            else {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
    protected int y;

    public int Y 
    { get { return y; } 
        set {
            if (value >= 0 && value <= 600)
            {
                x = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }

    public Punto(int x, int y)
    {
        this.x = x;
        this.y = y;
    }   

    public String punto(int x, int y)
    {
        this.x = x; 
        this.y = y;
        return "Punto inicial (" + x + ", " + y + ")";
    }


    public virtual Boolean Mover(int x, int y)
    {
        if((x >= 0 && x <= 800)&&(y >= 0 && y <= 600))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual String Dibujar()
    {
        return " se encuentra en el punto (" + x + ", " + y + ")";
    }
}

public class Circulo: Punto
{
    int radio;
    public int Radio
    {
        get { return radio; }
        set
        {
            if ((this.x - value >= 0 && this.x + value <= 800) && (this.y - value >= 0 && this.y + value <= 600))
            {
                radio = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }

    public Circulo(int x, int y, int radio): base(x, y)
    {
        this.x = x;
        this.y = y;
        Radio = radio;
    }

    public void circulo(int x, int y, int radio)
    {
        punto(x, y);

        radio = Radio;
    }

    public override Boolean Mover(int x, int y)
    {
        this.x = x;
        this.y = y;
        if (base.Mover(x + this.radio, y + this.radio) && base.Mover(x - this.radio, y - this.radio))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override String Dibujar()
    {
        String resultado = "";
        if(Mover(this.x, this.y)) { resultado = "El circulo se encuentra en el punto " + "(" + x + "," + y + ")"; }
        else
        {
            throw new ArgumentOutOfRangeException();
        }
        return resultado;
    }
}

public class Rectangulo: Punto
{
    protected int ancho;

    public int Ancho
    {
        get { return ancho; }
        set
        {
            if (this.x - value >= 0 && this.x + value <= 800)
            {
                ancho = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }


    protected int alto;

    public int Alto
    {
        get { return alto; }
        set
        {
            if (this.y - value >= 0 && this.y + value <= 600)
            {
                alto = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }

    public Rectangulo(int x, int y, int alto, int ancho): base(x, y)
    {
        this.x = x;
        this.y = y;
        Ancho = ancho;
        Alto = alto;
    }
    public void rectangulo(int x, int y, int ancho, int alto)
    {
        punto(x, y);
        ancho = Ancho;
        alto = Alto;
    }

    public override Boolean Mover(int x, int y)
    {
        this.x = x;
        this.y = y;

        if (base.Mover(x + this.ancho, y + this.alto) && base.Mover(x - this.ancho, y - this.alto))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override String Dibujar()
    {
        String resultado = "";
        if (Mover(this.x, this.y)) { resultado = "El rectángulo se encuentra en el punto " + "(" + x + "," + y + ")"; }
        else
        {
            throw new ArgumentOutOfRangeException();
        }
        return resultado;
    }
}