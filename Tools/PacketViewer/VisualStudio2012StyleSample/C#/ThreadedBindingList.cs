using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VisualStudio2012Style
{
    public class ThreadedBindingList<T> : BindingList<T>
    {
        private readonly SynchronizationContext m_syncContext = SynchronizationContext.Current;

        public ThreadedBindingList()
        {
        }

        public ThreadedBindingList(IEnumerable<T> list)
        {
            list.ToList().ForEach(v => Add(v));
        }

        //protected override void OnAddingNew(AddingNewEventArgs e)
        //{
        //    if (m_syncContext == null)
        //        BaseAddingNew(e);
        //    else
        //        m_syncContext.Send(state => BaseAddingNew(e), null);
        //}

        protected override void OnListChanged(ListChangedEventArgs e)
        {
            if (m_syncContext == null)
                base.OnListChanged(e);
            else
                m_syncContext.Send(state => BaseListChanged(e), null);

            System.Threading.Thread.Sleep(100);

        }

        private void BaseAddingNew(AddingNewEventArgs e)
        {
            base.OnAddingNew(e);
        }

        private void BaseListChanged(ListChangedEventArgs e)
        {
            base.OnListChanged(e);
        }
    }
}
