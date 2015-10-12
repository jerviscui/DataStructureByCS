using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CoreType.Define;

namespace Core.CoreType.Implement
{
    public class Graph<TV, TE> : IGraph<TV, TE> where TV : IComparable<TV> where TE : IComparable<TE>
    {
        private Vector<Vertex<TV>> _vertexes;

        private Vector<Vector<Edge<TE>>> _edges;

        private int _edgeCount;

        private int _vertexCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public Graph(Vector<Vertex<TV>> vertexes, Vector<Vector<Edge<TE>>> edges)
        {
            _vertexes = vertexes;
            _edges = edges;
            _vertexCount = vertexes.Size();
        }

        #region Edge Operation
        /// <summary>
        /// 判断边是否存在
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public bool Exists(int i, int j)
        {
            return (0 <= i && i < _vertexCount) && (0 <= j && j < _vertexCount) &&
                _edges[i][j] != null;
        }

        public TE Edge(int i, int j)
        {
            return _edges[i][j].Data;
        }

        public EStatus Status(int i, int j)
        {
            return _edges[i][j].Status;
        }

        public int Weight(int i, int j)
        {
            return _edges[i][j].Weight;
        }

        public void Insert(int i, int j, TE data, int weight)
        {
            if (Exists(i, j))
            {
                return;
            }

            _edges[i][j] = new Edge<TE>(data, weight);
            _edgeCount++;
            _vertexes[i].InDegree++;
            _vertexes[i].OutDegree++;
        }

        public TE Remove(int i, int j)
        {
            TE back = _edges[i][j].Data;
            _edges[i][j] = null;
            _edgeCount--;
            _vertexes[i].InDegree--;
            _vertexes[i].OutDegree--;

            return back;
        }
        #endregion

        #region Vertex Operation

        public TV Vertex(int i)
        {
            return _vertexes[i].Data;
        }

        public int InDegree(int i)
        {
            return _vertexes[i].InDegree;
        }

        public int OutDegree(int i)
        {
            return _vertexes[i].OutDegree;
        }

        public VStatus Status(int i)
        {
            return _vertexes[i].Status;
        }

        public int DisTime(int i)
        {
            return _vertexes[i].DisTime;
        }

        public int FTime(int i)
        {
            return _vertexes[i].FTime;
        }

        public int Parent(int i)
        {
            return _vertexes[i].Parent;
        }

        public int Priority(int i)
        {
            return _vertexes[i].Priority;
        }

        /// <summary>
        /// 顶点i枚举到邻居j，求下一个邻居
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public int NextNbr(int i, int j)
        {
            while (-1 < j && !Exists(i, --j))
            {
                ;
            }

            return j;
        }

        /// <summary>
        /// 顶点i的第一个邻居
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int FirstNbr(int i)
        {
            return NextNbr(i, _vertexes.Size());
        }

        public int Insert(TV data)
        {
            for (int i = 0; i < _vertexCount; i++)
            {
                _edges[i].Insert(null);
            }
            _vertexCount++;
            _edges.Insert(new Vector<Edge<TE>>(_vertexCount, _vertexCount, null));
            _vertexes.Insert(new Vertex<TV>(data));

            return _vertexCount - 1;
        }

        public TV Remove(int i)
        {
            //delete row
            for (int j = 0; j < _vertexCount; j++)
            {
                if (Exists(i, j))
                {
                    _edges[i][j] = null;
                    _vertexes[j].InDegree--;
                    //todo: edges count? changed
                    _edgeCount--;
                }
            }
            _edges.Remove(i);
            //delete column
            _vertexCount--;
            for (int j = 0; j < _vertexCount; j++)
            {
                if (Exists(j, i))
                {
                    _edges[j][i] = null;
                    _vertexes[j].OutDegree--;
                    _edgeCount--;
                }
                _edges[j].Remove(i);
            }

            //delete vertex
            TV back = _vertexes[i].Data;
            _vertexes.Remove(i);

            return back;
        }
        #endregion

        private void Reset()
        {
            for (int i = 0; i < _vertexCount; i++)
            {
                //vertexes infos reset
                //Status DisTime FTime Parent Priority
                _vertexes[i].Status = VStatus.Undiscovered;
                _vertexes[i].DisTime = -1;
                _vertexes[i].FTime = -1;
                _vertexes[i].Parent = -1;
                _vertexes[i].Priority = int.MaxValue;

                for (int j = 0; j < _vertexCount; j++)
                {
                    //edges infos reset
                    //Status
                    _edges[i][j].Status = EStatus.Undetermined;
                }
            }
        }

        #region Public

        public void Bfs(int v, ref int clock)
        {
            MyQueue<int> queue = new MyQueue<int>();
            queue.EnQueue(v);
            _vertexes[v].Status = VStatus.Discovered;

            while (!queue.Empty())
            {
                v = queue.DeQueue();
                _vertexes[v].DisTime = ++clock;
                for (int i = FirstNbr(v); -1 < i; i = NextNbr(v, i))
                {
                    if (_vertexes[i].Status == VStatus.Undiscovered)
                    {
                        queue.EnQueue(i);
                        _vertexes[i].Status = VStatus.Discovered;
                        _vertexes[i].Parent = v;
                        _edges[v][i].Status = EStatus.Tree;
                    }
                    else
                    {
                        _edges[v][i].Status = EStatus.Cross;
                    }
                }
                _vertexes[v].Status = VStatus.Visited;
            }
        }

        /// <summary>
        /// 如果图拥有多个连通域
        /// </summary>
        public void BfsAll(int s)
        {
            Reset();
            int clock = 0;
            int v = s;
            do
            {
                if (_vertexes[v].Status == VStatus.Undiscovered)
                {
                    Bfs(v, ref clock);
                }
            } while (s != (v = ++v % _vertexCount));
            // 该循环条件很有意思，确实完成了所有节点的遍历
        }

        public void Dfs(int v, ref int clock)
        {
            _vertexes[v].Status = VStatus.Discovered;
            _vertexes[v].DisTime = ++clock;

            for (int i = FirstNbr(v); -1 < i; i = NextNbr(v, i))
            {
                //不同于广度优先，深度会先找出单链的中点然后再寻找该位置的下一个邻接节点
                //所以此处的遍历是靠递归
                switch (_vertexes[i].Status)
                {
                    case VStatus.Undiscovered:
                        _vertexes[i].Status = VStatus.Discovered;
                        _edges[v][i].Status = EStatus.Tree;
                        Dfs(i, ref clock);
                        break;
                    case VStatus.Discovered:
                        _edges[v][i].Status = EStatus.Backward;
                        break;
                    //visited
                    default:
                        _edges[v][i].Status = _vertexes[v].DisTime < _vertexes[i].DisTime
                            ? EStatus.Forward
                            : EStatus.Cross;
                        break;
                }
            }

            _vertexes[v].Status = VStatus.Visited;
            _vertexes[v].FTime = ++clock;
        }

        public void DfsAll(int s)
        {
            Reset();
            int clock = 0;
            int v = s;
            do
            {
                if (_vertexes[v].Status == VStatus.Undiscovered)
                {
                    Dfs(v, ref clock);
                }
            } while (s != (v = ++v % _vertexCount));
        }
        #endregion
    }
}
