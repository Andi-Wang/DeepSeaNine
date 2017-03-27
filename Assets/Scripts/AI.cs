using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

	public static List<Vector3> aStar(Point start, Point end, string type) {

		SortedDictionary<Point, NodeInfo> open = new SortedDictionary<Point, NodeInfo>();
		SortedDictionary<Point, NodeInfo> closed = new SortedDictionary<Point, NodeInfo>();
		Vector3[] directions = new Vector3[]{ Vector3.up, Vector3.down, Vector3.left, Vector3.right };

		// add start to open
		open.Add(start, new NodeInfo(start, null, 0, 0));

		// iterate through open
		while (open.Count > 0) {
			KeyValuePair<Point, NodeInfo> q = new KeyValuePair<Point, NodeInfo>(); // RHS will get thrown away
			int minF = int.MaxValue;

			// pop entry with smallest f value
			foreach (KeyValuePair<Point, NodeInfo> entry in open) {
				if (entry.Value.f() < minF) {
					q = entry;
				}
			}
			open.Remove (q.Key);

			// iterate through each child
			foreach (Vector3 direction in directions) {
				Point successorPoint = q.Key.getPointInDirection (direction);
				int successorG = q.Value.g + 1;
				int successorH = Point.absoluteDistance (successorPoint, end);
				NodeInfo successorNodeInfo = new NodeInfo(successorPoint, q.Value, successorG, successorH);

				// did we find the end?
				if (successorPoint == end) {
					// give me directions to go from start to end
					List<Vector3> moves = new List<Vector3> ();
					NodeInfo n = successorNodeInfo;

					while (n.parent != null) {
						moves.Insert (0, n.parent.point.getDirectionTo(n.point));
						n = n.parent;
					}

					return moves;
				}

				// disregard tiles of which are non-travelable
				if (LevelManager.Instance.Tiles [successorPoint].Type != type) {
					continue;
				}

				// check if we have already found a better path to this point
				// add it if we have not
				if (open.ContainsKey (successorPoint)) {
					if (open [successorPoint].f() < successorNodeInfo.f()) {
						continue;
					}
				} else if (closed.ContainsKey (successorPoint)) {
					if (closed [successorPoint].f() < successorNodeInfo.f()) {
						continue;
					}
				} else {
					open.Add (successorPoint, successorNodeInfo);
				}
			} 

			// we have checked q
			closed.Add (q.Key, q.Value);

		}

		print ("A* failed! We are in trouble"); 
		return null;
	}
}
