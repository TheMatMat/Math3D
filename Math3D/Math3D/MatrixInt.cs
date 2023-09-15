using System;
using System.Runtime.CompilerServices;
using Maths_Matrices.Tests;

namespace Math3D
{
    public class MatrixInt
    {
        private int nbLines;
        private int nbColumns;
        private int[,] matrixArray;
        
        //Properties
        public int NbLines
        {
            get => nbLines;
            private set => nbLines = value;
        }

        public int NbColumns
        {
            get => nbColumns;
            private set => nbColumns = value;
        }

        public int[,] MatrixArray
        {
            get => matrixArray;
            set => matrixArray = value;
        }
        
        //Indexer
        public int this[int i, int j]
        {
            get => matrixArray[i, j];
            set => matrixArray[i, j] = value;
        }
        
        //Operator
        public static MatrixInt operator -(MatrixInt m)
        {
            return MatrixInt.Multiply(m, -1);
        }
        
        public static MatrixInt operator -(MatrixInt m1, MatrixInt m2)
        {
            return MatrixInt.Add(m1, -m2);
        }

        public static MatrixInt operator +(MatrixInt m1, MatrixInt m2)
        {
            return Add(m1, m2);
        }
        
        public static MatrixInt operator *(MatrixInt m, int i)
        {
            return MatrixInt.Multiply(m, i);
        } 
        
        public static MatrixInt operator *(int i, MatrixInt m)
        {
            return MatrixInt.Multiply(m, i);
        }
        
        public static MatrixInt operator *(MatrixInt m1, MatrixInt m2)
        {
            return MatrixInt.Multiply(m1, m2);
        }
        
        // Constructor
        public MatrixInt(int line, int column)
        {
            nbLines = line;
            nbColumns = column;

            matrixArray = new int[nbLines, nbColumns];
        }
        
        public MatrixInt(int[,] array)
        {
            nbLines = array.GetLength(0);
            nbColumns = array.GetLength(1);
            
            matrixArray = new int[nbLines, nbColumns];

            for (int i = 0; i < nbLines; i++)
            {
                for (int j = 0; j < nbColumns; j++)
                {
                    matrixArray[i, j] = array[i, j];
                }
            }
        }

        public MatrixInt(MatrixInt m)
        {
            nbLines = m.nbLines;
            nbColumns = m.nbColumns;

            matrixArray = new int[nbLines, nbColumns];

            for (int i = 0; i < nbLines; i++)
            {
                for (int j = 0; j < nbColumns; j++)
                {
                    matrixArray[i, j] = m.matrixArray[i, j];
                }
            }
        }

        public int[,] ToArray2D()
        {
            return matrixArray;
        }

        public static MatrixInt Identity(int size)
        {
            int[,] identity = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if(i == j)
                        identity[i, j] = 1;
                    else
                        identity[i, j] = 0;
                }
            }

            return new MatrixInt(identity);
        }

        public bool IsIdentity()
        {
            //guard: 2D array isn't set
            if (nbLines == 0 || nbColumns == 0)
                return false;
            
            //not the a square matrix so not identity
            if (nbLines != nbColumns)
                return false;
            
            for (int i = 0; i < nbLines; i++)
            {
                for (int j = 0; j < nbColumns; j++)
                {
                    if ((i != j && matrixArray[i, j] != 0) || (i == j && matrixArray[i, j] != 1))
                        return false;
                }
            }

            return true;
        }

        public void Multiply(int scalar)
        {
            for (int i = 0; i < nbLines; i++)
            {
                for (int j = 0; j < nbColumns; j++)
                {
                    matrixArray[i, j] *= scalar;
                }
            }
        }

        public MatrixInt Multiply(MatrixInt m)
        {
            if (nbColumns != m.nbLines)
                throw new MatrixMultiplyException();
                
            int[,] newMatrix = new int[nbLines, m.nbColumns];

            for (int i = 0; i < nbLines; i++)
            {
                for (int j = 0; j < m.nbColumns; j++)
                {
                    int result = 0;

                    for (int k = 0; k < nbColumns; k++)
                    {
                        result += matrixArray[i, k] * m.matrixArray[k, j];
                    }

                    newMatrix[i, j] = result;
                }
            }

            return new MatrixInt(newMatrix);
        }

        public static MatrixInt Multiply(MatrixInt m, int scalar)
        {
            MatrixInt newMatrix = new MatrixInt(m);
            newMatrix.Multiply(scalar);

            return newMatrix;
        }
        
        public static MatrixInt Multiply(MatrixInt m1, MatrixInt m2)
        {
            if (m1.nbColumns != m2.nbLines)
                throw new MatrixMultiplyException();
            
            MatrixInt newMatrix = new MatrixInt(m1);
            newMatrix.Multiply(m2);

            return newMatrix.Multiply(m2);
        }

        public void Add(MatrixInt m2)
        {
            if (nbColumns != m2.nbColumns || nbLines != m2.nbLines)
                throw new MatrixSumException();
            
            for (int i = 0; i < nbLines; i++)
            {
                for (int j = 0; j < nbColumns; j++)
                {
                    matrixArray[i, j] += m2.matrixArray[i, j];
                }
            }
        }

        public static MatrixInt Add(MatrixInt m1, MatrixInt m2)
        {
            if (m1.nbLines != m2.nbLines || m1.NbColumns != m2.nbColumns)
                throw new MatrixSumException();

            MatrixInt newMatrix = new MatrixInt(m1);
            newMatrix.Add(m2);

            return newMatrix;
        }

        public MatrixInt Transpose()
        {
            int[,] newMatrixArray = new int[nbColumns, nbLines];

            for (int i = 0; i < nbColumns; i++)
            {
                for (int j = 0; j < nbLines; j++)
                {
                    newMatrixArray[i, j] = matrixArray[j, i];
                }
            }

            return new MatrixInt(newMatrixArray);
        }

        public static MatrixInt Transpose(MatrixInt m)
        {
            return m.Transpose();
        }

        public static MatrixInt GenerateAugmentedMatrix(MatrixInt m1, MatrixInt m2)
        {
            MatrixInt augmentedMatrix = new MatrixInt(m1.nbLines, m1.NbColumns + m2.NbColumns);

            for (int i = 0; i < augmentedMatrix.nbLines; i++)
            {
                for (int j = 0; j < augmentedMatrix.nbColumns; j++)
                {
                    if (j < m1.nbColumns)
                        augmentedMatrix[i, j] = m1.matrixArray[i, j];
                    else
                        augmentedMatrix[i, j] = m2.matrixArray[i, j - m1.nbColumns];
                }
            }

            return augmentedMatrix;
        }

        public (MatrixInt, MatrixInt) Split(int columnIndex)
        {
            MatrixInt m1 = new MatrixInt(nbLines, columnIndex + 1);
            MatrixInt m2 = new MatrixInt(nbLines, nbColumns - m1.NbColumns);
            
            //fill m1
            for (int i = 0; i < m1.nbLines; i++)
            {
                for (int j = 0; j < m1.nbColumns; j++)
                {
                    m1.matrixArray[i, j] = matrixArray[i, j];
                }
            }
            
            //fill m2
            for (int i = 0; i < m2.nbLines; i++)
            {
                for (int j = 0; j < m2.nbColumns; j++)
                {
                    m2.matrixArray[i, j] = matrixArray[i, j + columnIndex + 1];
                }
            }

            return (m1, m2);
        }
    }
    
    public class MatrixSumException : SystemException
    {
        public MatrixSumException()
        {
            Console.WriteLine("Matrice Sum Failed: matrix must be the same size!");
        }
    }
    
    public class MatrixMultiplyException : SystemException
    {
        public MatrixMultiplyException()
        {
            Console.WriteLine("Matrice Multiply Failed: number of column of the 1st matrix must be the same as number of line of the 2nd one!");
        }
    }

}

