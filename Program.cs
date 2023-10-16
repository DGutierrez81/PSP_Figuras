// See https://aka.ms/new-console-template for more information

using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

public class Principal
{
    public static void Main(string[] args)
    {
        Circulo circulo = new Circulo();

        circulo.punto(44, 60);

        circulo.Radio = 20;

        Rectangulo rectangulo = new Rectangulo();

        try
        {
            rectangulo.punto(803, 700);

            rectangulo.Alto = 30;

            rectangulo.Ancho = 100;
        } catch(ArgumentOutOfRangeException)
        {
            Console.WriteLine("Ha dado error, como debe");
        }
        GraficoCompuesto compuesto = new GraficoCompuesto();

        compuesto.Aniadir(circulo);

        compuesto.Aniadir(rectangulo);

        compuesto.Mover(600, 70);

        compuesto.Dibujar();

        Console.WriteLine("Menu: \n1 Mover gráfico.\n2 Salir");

        String eleccion = Console.ReadLine();

        try
        {
            switch (eleccion)
            {
                case "1":
                    Console.WriteLine("¿Qué gráfico desea mover?\n1 Circulo.\n2 Rectángulo.\n3 Todos.");
                    String respuesta = Console.ReadLine();
                    switch (respuesta)
                    {
                        case "1":
                            Console.WriteLine("Escriba el valor de la coordenada x: ");
                            int xCirulo = int.Parse(Console.ReadLine());
                            Console.WriteLine("Escriba el valor de la coordenada y: ");
                            int yCirulo = int.Parse(Console.ReadLine());
                            circulo.Mover(xCirulo, yCirulo);
                            Console.WriteLine(circulo.Dibujar());
                            break;
                        case "2":
                            Console.WriteLine("Escriba el valor de la coordenada x: ");
                            int xRectangulo = int.Parse(Console.ReadLine());
                            Console.WriteLine("Escriba el valor de la coordenada y: ");
                            int yRectangulo = int.Parse(Console.ReadLine());
                            rectangulo.Mover(xRectangulo, yRectangulo);
                            Console.WriteLine(rectangulo.Dibujar());
                            break;
                        case "3":
                            Console.WriteLine("Escriba el valor de la coordenada x: ");
                            int xCompuesto = int.Parse(Console.ReadLine());
                            Console.WriteLine("Escriba el valor de la coordenada y: ");
                            int yCompuesto = int.Parse(Console.ReadLine());
                            compuesto.Mover(xCompuesto, yCompuesto);
                            Console.WriteLine(compuesto.Dibujar());
                            break;
                    }
                    break;
                case "2":
                    Console.WriteLine("Fin del programa.");
                    break;
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Puntos o figura fuera de rango.");
        }

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

    public GraficoCompuesto(List<IGrafico> graficos)
    {
        this.graficos = graficos;
    }

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
        X = x;
        X = x;
    }   

    public String punto(int x, int y)
    {
        this.x = x; 
        this.y = y;
        return "Punto inicial (" + x + ", " + y + ")";
    }


    public virtual Boolean Mover(int x, int y)
    {
        if((x >= 0 && x <= 800))
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

    public Circulo(int radio): base(x, y)
    {
        Radio = radio;
    }

    public void circulo(int x, int y, int radio)
    {
        punto(x, y);

        this.radio = radio;
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
        if(Mover(this.x, this.y)) { resultado = "El circulo se ha desplazado al punto " + "(" + x + "," + y + ")"; }
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

    public void rectangulo(int x, int y, int ancho, int alto)
    {
        punto(x, y);
        this.ancho = ancho;
        this.alto = alto;
    }

    public override Boolean Mover(int x, int y)
    {
        this.x = x;
        this.y = y;

        if (base.Mover(x + ancho, y + alto) && base.Mover(x - ancho, y - alto))
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
        if (Mover(this.x, this.y)) { resultado = "El rectángulo se ha desplazado al punto " + "(" + x + "," + y + ")"; }
        else
        {
            throw new ArgumentOutOfRangeException();
        }
        return resultado;
    }
}