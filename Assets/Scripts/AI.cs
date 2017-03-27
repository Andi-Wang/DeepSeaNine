using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

	public static List<Vector3> aStar(Point start, Point end, string type) {
		Dictionary<Point, NodeInfo> open = new Dictionary<Point, NodeInfo>();
		Dictionary<Point, NodeInfo> closed = new Dictionary<Point, NodeInfo>();
		Vector3[] directions = new Vector3[]{ Vector3.up, Vector3.down, Vector3.left, Vector3.right };

		// add start to open
		open.Add(start, new NodeInfo(start, null, 0, 0));

		// iterate through open
		while (open.Count > 0) {
			KeyValuePair<Point, NodeInfo> q = new KeyValuePair<Point, NodeInfo>(); // RHS will get thrown away
			int minF = int.MaxValue;

			// pop an entry with smallest f value
			List<KeyValuePair<Point, NodeInfo>> qCandidates = new List<KeyValuePair<Point, NodeInfo>>();
			foreach (KeyValuePair<Point, NodeInfo> entry in open) {
				if (entry.Value.f () < minF) {
					qCandidates = new List<KeyValuePair<Point, NodeInfo>> ();
					qCandidates.Add(new KeyValuePair<Point, NodeInfo> (entry.Key, entry.Value));
					minF = entry.Value.f ();
				} else if (entry.Value.f () == minF) {
					qCandidates.Add(new KeyValuePair<Point, NodeInfo> (entry.Key, entry.Value));
				}
			}
			q = qCandidates[(int)System.Math.Floor ((float)UnityEngine.Random.Range (0, qCandidates.Count))];
			open.Remove (q.Key);

			// iterate through each child
			foreach (Vector3 direction in directions) {
				Point successorPoint = q.Key.getPointInDirection (direction);
				int successorG = q.Value.g + 1;
				int successorH = Point.absoluteDistance (successorPoint, end);
				NodeInfo successorNodeInfo = new NodeInfo(successorPoint, q.Value, successorG, successorH);

				// skip tiles not on board
				if (!LevelManager.Instance.Tiles.ContainsKey (successorPoint)) {
					continue;
				}

				// did we find the end?
				if (successorPoint == end) {
					// give me directions to go from start to end
					List<Vector3> moves = new List<Vector3> ();
					NodeInfo n = successorNodeInfo;

					while (n.parent != null) {
						Vector3 dir = n.parent.point.getDirectionTo (n.point);
						moves.Insert (0, dir);
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
