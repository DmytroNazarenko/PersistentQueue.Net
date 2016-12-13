using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistentDataStructures
{
    class PersistentQueue<T>
    {
        private PersistentStack<T> L;
        private PersistentStack<T> Li;
        private PersistentStack<T> R;
        private PersistentStack<T> Ri;
        private PersistentStack<T> S;

        private Boolean recopy;
        private Int32 toCopy;
        private Boolean copied;

        public Boolean Empty
        {
            get
            {
                return !recopy && R.Count == 0;
            }
        }

        private PersistentQueue(
            PersistentStack<T> newL, 
            PersistentStack<T> newLi, 
            PersistentStack<T> newR, 
            PersistentStack<T> newRi, 
            PersistentStack<T> newS,
            Boolean newRecopy,
            Int32 newToCopy,
            Boolean newCopied) : this()
        {
            L = newL;
            Li = newLi;
            R = newR;
            Ri = newRi;
            S = newS;

            recopy = newRecopy;
            toCopy = newToCopy;
            copied = newCopied;
        }

        public PersistentQueue()
        {
            L = new PersistentStack<T>();
            Li = new PersistentStack<T>();
            R = new PersistentStack<T>();
            Ri = new PersistentStack<T>();
            S = new PersistentStack<T>();

            recopy = false;
            toCopy = 0;
            copied = false;
        }

        public PersistentQueue<T> Push(T element)
        {
            if (!recopy)
            {
                PersistentStack<T> Ln = L.Push(element);
                PersistentQueue<T> Q = new PersistentQueue<T>(Ln, Li, R, Ri, S, recopy, toCopy, copied);
                return Q.CheckRecopy();
            }
            else
            {
                PersistentStack<T> Lni = Li.Push(element);
                PersistentQueue<T> Q = new PersistentQueue<T>(L, Lni, R, Ri, S, recopy, toCopy, copied);
                return Q.CheckNormal();
            }
        }

        public PersistentQueue<T> Pop()
        {
            if (!recopy)
            {
                PersistentStack<T> Rn = R.Pop();
                PersistentQueue<T> Q = new PersistentQueue<T>(L, Li, Rn, Ri, S, recopy, toCopy, copied);
                return Q.CheckRecopy();
            }
            else
            {
                PersistentStack<T> Rni = Ri.Pop();
                PersistentStack<T> Rn = R;
                Int32 curCopy = toCopy;
                if (toCopy > 0)
                {
                    --curCopy;
                }
                else
                {
                    Rn = Rn.Pop();
                }
                PersistentQueue<T> Q = new PersistentQueue<T>(L, Li, Rn, Rni, S, recopy, curCopy, copied);
                return Q.CheckNormal();
            }
        }

        public T Peek()
        {
            if (!recopy)
            {
                return R.Peek();
            }
            else
            {
                return Ri.Peek();
            }
        }

        private PersistentQueue<T> CheckRecopy()
        {
            if (L.Count > R.Count)
            {
                PersistentQueue<T> Q = new PersistentQueue<T>(L, Li, R, R, S, true, R.Count, false);
                return Q.CheckNormal();
            }
            else
            {
                return new PersistentQueue<T>(L, Li, R, Ri, S, false, toCopy, copied);
            }
        }

        private PersistentQueue<T> CheckNormal()
        {
            PersistentQueue<T> Q = this.AdditionalsOperations();
            return new PersistentQueue<T>(Q.L, Q.Li, Q.R, Q.Ri, Q.S, Q.S.Count != 0, Q.toCopy, Q.copied);
        }

        private PersistentQueue<T> AdditionalsOperations()
        {
            Int32 toDo = 3;

            PersistentStack<T> Ln = L;
            PersistentStack<T> Lni = Li;
            PersistentStack<T> Rn = R;
            PersistentStack<T> Sn = S;

            Boolean curCopied = copied;
            Int32 curCopy = toCopy;

            while (!curCopied && toDo > 0 && Rn.Count > 0)
            {
                T x = Rn.Peek();
                Rn = Rn.Pop();
                Sn = Sn.Push(x);
                --toDo;
            }

            while (toDo > 0 && Ln.Count > 0)
            {
                curCopied = true;
                T x = Ln.Peek();
                Ln = Ln.Pop();
                Rn = Rn.Push(x);
                --toDo;
            }

            while (toDo > 0 && Sn.Count > 0)
            {
                T x = Sn.Peek();
                Sn = Sn.Pop();
                if (curCopy > 0)
                {
                    Rn = Rn.Push(x);
                    --curCopy;
                }
                --toDo;
            }

            if (Sn.Count == 0)
            {
                PersistentStack<T> t = Ln;
                Ln = Lni;
                Lni = t;
            }

            return new PersistentQueue<T>(Ln, Lni, Rn, Ri, Sn, recopy, curCopy, curCopied);
        }
    }
}
