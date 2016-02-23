namespace crm.logic
{
    public class State<T>
    {
        private T state;

        public void Put(T state) {
            this.state = state;
        }

        public T Get() {
            return state;
        }
    }
}