using System;
using FluentNHibernate.Mapping;


namespace FluentNHibernate.Example
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var fordMake = new Make
                    {
                        Name = "Ford"
                    };
                    var fiestaModel = new Model
                    {
                        Name = "Fiesta",
                        Make = fordMake
                    };

                    var car = new Car
                    {
                        Make = fordMake,
                        Model = fiestaModel,
                        Title = "Dans Car"
                    };

                    session.Save(car);

                    transaction.Commit();

                    Console.WriteLine("Created Car " + car.Title);
                }
            }
            Console.ReadLine();
        }
    }


    public class MakeMap : ClassMap<Make>
    {
        public MakeMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);

        }
    }

    public class ModelMap : ClassMap<Model>
    {
        public ModelMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            References(x => x.Make).Cascade.All();

        }
    }

    public class CarMap : ClassMap<Car>
    {
        public CarMap()
        {
            Id(x => x.Id);
            Map(x => x.Title);
            References(x => x.Make).Cascade.All();
            References(x => x.Model).Cascade.All();

        }
    }

    public class Make
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }

    public class Model
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual Make Make { get; set; }
    }

    public class Car
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual Make Make { get; set; }
        public virtual Model Model { get; set; }
    }

}
