using System;
using System.Collections.Generic;

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
        }
        catch (FormatException)
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
                    int xCirculo = int.Parse(Console.ReadLine());
                    Console.WriteLine("Escriba el nuevo valor de la coordenada y: ");
                    int yCirculo = int.Parse(Console.ReadLine());
                    circulo.Mover(xCirculo, yCirculo);
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

public class GraficoCompuesto : IGrafico
{
    private List<IGrafico> graficos = new List<IGrafico>();

    public Boolean Mover(int x, int y)
    {
        var retorno = true;
        for (int i = 0; i < graficos.Count; i++)
        {
            if (!graficos[i].Mover(x, y))
            {
                retorno = false;
            }
        }
        return retorno;
    }

    public String Dibujar()
    {
        String resultado = "";
        for (int i = 0; i < graficos.Count; i++)
        {
            resultado += graficos[i].Dibujar() + "\n";
        }
        return resultado;
    }

    public void Aniadir(IGrafico grafico)
    {
        graficos.Add(grafico);
    }
}

public class Punto : IGrafico
{
    protected int x;
    protected int y;

    public Punto(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public Boolean Mover(int x, int y)
    {
        this.x = x;
        this.y = y;
        return true; // Simplemente actualiza las coordenadas del punto
    }

    public virtual String Dibujar()
    {
        return "Punto se encuentra en el punto (" + x + ", " + y + ")";
    }
}

public class Circulo : Punto
{
    int radio;

    public Circulo(int x, int y, int radio) : base(x, y)
    {
        this.radio = radio;
    }

    public virtual Boolean Mover(int x, int y)
    {
        this.x = x;
        this.y = y;
        return true; // Simplemente actualiza las coordenadas del punto
    }

    public override String Dibujar()
    {
        return "El círculo se encuentra en el punto (" + x + "," + y + ") con radio " + radio;
    }
}

public class Rectangulo : Punto
{
    protected int ancho;
    protected int alto;

    public Rectangulo(int x, int y, int alto, int ancho) : base(x, y)
    {
        this.alto = alto;
        this.ancho = ancho;
    }

    public virtual Boolean Mover(int x, int y)
    {
        this.x = x;
        this.y = y;
        return true; // Simplemente actualiza las coordenadas del punto
    }

    public override String Dibujar()
    {
        return "El rectángulo se encuentra en el punto (" + x + "," + y + ") con ancho " + ancho + " y alto " + alto;
    }
}