namespace HitachiTask;

public sealed class DijkstraStrategy : SSSPStrategy {
    private static readonly int[] Dr = {-1, 1, 0, 0};
    private static readonly int[] Dc = {0, 0, -1, 1};
    
    public PathResultDto FindPath(Grid grid, Position source, Position destination) {
        var pq = new PriorityQueue<Position, int>();
        var distance = new Dictionary<Position, int>();
        var parent = new Dictionary<Position, Position>();

        distance[source] = 0;
        pq.Enqueue(source, 0);

        while (pq.Count > 0) {
            var current = pq.Dequeue();

            if (current == destination) {
                break;
            }

            for (int i = 0; i < 4; i++) {
                var next = new Position(current.Row + Dr[i], current.Col + Dc[i]);

                if (!grid.IsInside(next)) {
                    continue;
                }

                if (!grid.GetTile(next).IsWalkable) {
                    continue;
                }

                int newDistance = distance[current] + 1;
                if (!distance.ContainsKey(next) || newDistance < distance[next])
                {
                    distance[next] = newDistance;
                    parent[next] = current;
                    pq.Enqueue(next, newDistance);
                }
            }
        }

        if (!distance.ContainsKey(destination)) {
            return new PathResultDto { Found = false };
        }

        var path = ReconstructPath(source, destination, parent);
        return new PathResultDto { Found = true, Distance = distance[destination], Path = path };
    }

    private static List<Position> ReconstructPath(Position source, Position destination, Dictionary<Position, Position> parent) {
        var path = new List<Position>();
        var current = destination;

        path.Add(current);
        while (current != source) {
            current = parent[current];
            path.Add(current);
        }

        path.Reverse();
        return path;
    }
}