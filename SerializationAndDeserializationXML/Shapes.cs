using System.Xml.Serialization;

namespace Exercise
{
    [XmlInclude(typeof(Rectangle))]
    [XmlInclude(typeof(Circle))]
    public abstract class Shape
    {
        [XmlAttribute("colour")]
        public string Colour;
        [XmlAttribute("area")]
        public abstract double Area { get; }
    }

    [XmlInclude(typeof(Square))]
    public class Rectangle : Shape
    {
        [XmlAttribute("hight")]
        public double Height { get; set; }
        [XmlAttribute("width")]
        public double Width { get; set; }
        public override double Area => Height * Width;
        public Rectangle()
        {
        }
        public Rectangle(double s)
        {
            this.Height = s;
            this.Width = s;
        }
        public Rectangle(double s, double w)
        {
            this.Height = s;
            this.Width = w;
        }
        public Rectangle(double s, string col)
        {
            this.Height = s;
            this.Width = s;
            this.Colour = col;
        }
        public Rectangle(double s, double w, string col)
        {
            this.Height = s;
            this.Width = w;
            this.Colour = col;
        }
    }

    public class Square : Rectangle
    {
        public Square() { }
        public Square(double side)
        {
            this.Height = side;
            this.Width = side;
        }
    }
    public class Circle : Shape
    {
        [XmlAttribute("rad")]
        public double Radius;
        public override double Area => System.Math.PI * this.Radius * this.Radius;
        public Circle()
        {
        }
        public Circle(double radius)
        {
            this.Radius = radius;
        }
        public Circle(double radius, string col)
        {
            this.Radius = radius;
            this.Colour = col;

        }
    }
}