namespace ILGPU_Functional_Base.Models
{
    // the contents of everything in this must be non-nullable
    public struct MyData
    {
        public int A;
        public int B;
        public int C;

        // Do some work
        public void UseData()
        {
            C = A * B;
        }
    }
}
