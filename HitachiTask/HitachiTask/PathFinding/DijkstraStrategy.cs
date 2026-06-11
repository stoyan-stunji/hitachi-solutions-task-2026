namespace HitachiTask;

public sealed class DijkstraStrategy : SSSPStrategy
{
    private static readonly Position[] Directions = {
        new(-1, 0), 
        new(1, 0),  
        new(0, -1), 
        new(0, 1)   
    };

    public PathResultDto FindPath(Grid grid, Position source, Position destination) {
        var pq = new PriorityQueue<Position, int>();
        var distance = new Dictionary<Position, int>();
        var parent = new Dictionary<Position, Position>();

        Initialize(source, pq, distance);
        while (pq.Count > 0) {
            var current = pq.Dequeue();
            if (ReachedDestination(current, destination)) {
                break;
            }
            ProcessNeighbors(current, grid, pq, distance, parent);
        }
        
        return BuildResult(source, destination, distance, parent);
    }
    
    private static void Initialize(Position source, PriorityQueue<Position, int> pq, 
        Dictionary<Position, int> distance) {
        distance[source] = 0;
        pq.Enqueue(source, 0);
    }
    
    private static bool ReachedDestination(Position current, Position destination) {
        return current == destination;
    }

    private void ProcessNeighbors(Position current, Grid grid, PriorityQueue<Position, int> pq,
        Dictionary<Position, int> distance, Dictionary<Position, Position> parent) {
        foreach (var dir in Directions) {
            Position next = new(current.Row + dir.Row, current.Col + dir.Col);
            if (!grid.IsInside(next)) {
                continue;
            }
            
            Tile tile = grid.GetTile(next);
            if (!tile.IsWalkable) {
                continue;
            }

            RelaxEdge(current, next, tile, pq, distance, parent);
        }
    }

    private static void RelaxEdge(Position current, Position next, Tile tile, PriorityQueue<Position, int> pq, 
        Dictionary<Position, int> distance, Dictionary<Position, Position> parent) {
        int newDistance = distance[current] + tile.MovementCost;

        if (!distance.ContainsKey(next) || newDistance < distance[next]) {
            distance[next] = newDistance;
            parent[next] = current;
            pq.Enqueue(next, newDistance);
        }
    }

    private static PathResultDto BuildResult(Position source, Position destination,
        Dictionary<Position, int> distance, Dictionary<Position, Position> parent) {
        if (!distance.ContainsKey(destination)) {
            return new PathResultDto { Found = false };
        }

        return new PathResultDto { Found = true, Distance = distance[destination],
            Path = ReconstructPath(source, destination, parent)
        };
    }
    
    private static List<Position> ReconstructPath(Position source, Position destination, 
        Dictionary<Position, Position> parent) {
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