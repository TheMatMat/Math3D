using System;

namespace Math3D
{
    public class MatrixElementaryOperations
    {
        public static void SwapLines(MatrixInt m, int l1, int l2)
        {
            for (int i = 0; i < m.NbColumns; i++)
            {
                int temp = m.MatrixArray[l1, i];
                m.MatrixArray[l1, i] = m.MatrixArray[l2, i];
                m.MatrixArray[l2, i] = temp;
            }
        }
        
        public static void SwapColumns(MatrixInt m, int c1, int c2)
        {
            for (int i = 0; i < m.NbLines; i++)
            {
                int temp = m.MatrixArray[i, c1];
                m.MatrixArray[i, c1] = m.MatrixArray[i, c2];
                m.MatrixArray[i, c2] = temp;
            }
        }


        public static void MultiplyLine(MatrixInt m, int lineIndex, int scalar)
        {
            if (scalar == 0)
                throw new MatrixScalarZeroException();
            
            for (int i = 0; i < m.NbColumns; i++)
            {
                m.MatrixArray[lineIndex, i] *= scalar;
            }
        }

        public static void MultiplyColumn(MatrixInt m, int columnIndex, int scalar)
        {
            if (scalar == 0)
                throw new MatrixScalarZeroException();
            
            for (int i = 0; i < m.NbLines; i++)
            {
                m.MatrixArray[i, columnIndex] *= scalar;
            }
        }

        public static void AddLineToAnother(MatrixInt m, int l1, int l2, int factor)
        {
            for (int i = 0; i < m.NbColumns; i++)
            {
                m.MatrixArray[l2, i] += m.MatrixArray[l1, i] * factor;
            }
        }

        public static void AddColumnToAnother(MatrixInt m, int c1, int c2, int factor)
        {
            for (int i = 0; i < m.NbLines; i++)
            {
                m.MatrixArray[i, c2] += m.MatrixArray[i, c1] * factor;
            }
        }
    }
    
    public class MatrixScalarZeroException : SystemException
    {
    }
}