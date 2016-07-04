using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mine.test.web.Models
{
    public class Class1
    {
        public Action<int> clusore()
        {
            int b = 0;
            return (i) => { b = i; };
        }

        public void test() {
            var clu = clusore();
            clu(1);
        }
    }

    public class Pizza { }
    public class CheesePizza : Pizza { }
    public class PepperonPizza : Pizza { }

    public class SimplePizzaFactory
    {
        public Pizza CreatePizza(string type)
        {
            if (type == "cheese")
                return new CheesePizza();
            else if (type == "pepperon")
                return new PepperonPizza();
            else return null;
        } 
    }

    public class PizzaSotre
    {
        private readonly SimplePizzaFactory _facotry;
        public PizzaSotre(SimplePizzaFactory facotry)
        {
            this._facotry = facotry;
        }

        public Pizza OrderPizza(string type)
        {
            var pizza = _facotry.CreatePizza(type);
            //对pizza做其他的后续料理
            return pizza;
        }
    }

    public abstract class PizzaStore2
    {
        public Pizza OrderPizza(string type)
        {
            var pizza = CreatePizza(type);
            // 
            return pizza;
        }

       protected abstract Pizza CreatePizza(string type);
    }

    public class NYPizzaStore2 : PizzaStore2
    {
        protected override Pizza CreatePizza(string type)
        {
            throw new NotImplementedException();
        }
    }

    public class HBPizzaStore2 : PizzaStore2
    {
        protected override Pizza CreatePizza(string type)
        {
            throw new NotImplementedException();
        }
    }

}