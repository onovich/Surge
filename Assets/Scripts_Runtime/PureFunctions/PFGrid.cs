using System;
using System.Collections.Generic;
using UnityEngine;

namespace Surge {

    public static class PFGrid {

        static Vector2Int[] tempArray = new Vector2Int[256 * 256];
        static HashSet<Vector2Int> tempHashSet = new HashSet<Vector2Int>(100 * 100);

        public static int Cells_InSize(Vector2Int leftBottom, Vector2Byte size, out Vector2Int[] cells) {
            int count = 0;
            cells = tempArray;
            for (int x = 0; x < size.x; x += 1) {
                for (int y = 0; y < size.y; y += 1) {
                    cells[count++] = leftBottom + new Vector2Int(x, y);
                }
            }
            return count;
        }

        public static Vector2 Cell_CollAndSprCenterOffset(Vector2Byte size) {
            Vector2 sizeFloat = size;
            return sizeFloat / 2f - Vector2.one / 2f;
        }

        public static void CellsBetweenTwoPoints(Vector2Int start, Vector2Int end, Action<Vector2Int> point) {
            int dx = Mathf.Abs(end.x - start.x);
            int dy = Mathf.Abs(end.y - start.y);
            int sx = start.x < end.x ? 1 : -1;
            int sy = start.y < end.y ? 1 : -1;
            int err = dx - dy;
            while (true) {
                if (start.x == end.x && start.y == end.y) {
                    break;
                }
                point(start);
                int e2 = 2 * err;
                if (e2 > -dy) {
                    err -= dy;
                    start.x += sx;
                }
                if (e2 < dx) {
                    err += dx;
                    start.y += sy;
                }
            }
        }

        public static int RectCycle_Grids(Vector2Int center, int cycle, out Vector2Int[] cells) {
            int count = 0;
            for (int x = -cycle; x <= cycle; x++) {
                for (int y = -cycle; y <= cycle; y++) {
                    tempArray[count] = new Vector2Int(center.x + x, center.y + y);
                    count++;
                }
            }
            cells = tempArray;
            return count;
        }

        public static HashSet<Vector2Int> RectCycle_GridHashSet(Vector2Int center, int cycle) {
            tempHashSet.Clear();
            for (int x = -cycle; x <= cycle; x++) {
                for (int y = -cycle; y <= cycle; y++) {
                    tempHashSet.Add(new Vector2Int(center.x + x, center.y + y));
                }
            }
            return tempHashSet;
        }

        public static void SpiralRectCycle_NearestGrid(Vector2Int center, int cycleCount, Predicate<Vector2Int> condition) {
            int curCycle = 0;
            int cx = center.x;
            int cy = center.y;
            while (curCycle <= cycleCount) {
                SpiralRectCycle_OneCycleNearestGrid(center, curCycle, condition);
                curCycle += 1;
            }
        }

        /// <summary>
        /// 从中心点开始, 按给定的圈数, 螺旋向外查找格子. 
        /// 离中心点近的位置, 排在数组的前面
        /// </summary>
        public static int SpiralRectCycle_Grids(Vector2Int center, int cycleCount, out Vector2Int[] grids) {
            // means: gridCount == 1
            // 2 1 8
            // 3 0 7
            // 4 5 6

            // 九宫格 center(05,10)
            // 04,11 05,11 06,11
            // 04,10 05,10 06,10
            // 04,09 05,09 06,09
            int curCycle = 0;
            int count = 0;
            int cx = center.x;
            int cy = center.y;
            while (curCycle <= cycleCount) {
                SpiralRectCycle_OneCycleGrids(center, curCycle, pos => {
                    tempArray[count++] = pos;
                });
                curCycle += 1;
            }
            grids = tempArray;
            return count;
        }

        /// <summary> 
        /// 第几圈
        /// </summary>
        // 2 2 2 2 2
        // 2 1 1 1 2
        // 2 1 0 1 2
        // 2 1 1 1 2
        // 2 2 2 2 2
        public static int SpiralRectCycle_OneCycleGrids(in Vector2Int center, int cycle, Action<Vector2Int> action) {
            int cx = center.x;
            int cy = center.y;
            int x;
            int y;
            int count = 0;
            // x ← o
            // o o o
            // o o o
            for (x = cx, y = cy + cycle; x >= cx - cycle; x -= 1) {
                action(new Vector2Int(x, y));
                count++;
            }
            // o o o
            // ↓ o o
            // x o o
            for (x = cx - cycle, y = cy + cycle - 1; y >= cy - cycle; y -= 1) {
                action(new Vector2Int(x, y));
                count++;
            }
            // o o o
            // o o o
            // o → x
            for (x = cx - cycle + 1, y = cy - cycle; x <= cx + cycle; x += 1) {
                action(new Vector2Int(x, y));
                count++;
            }
            // o o x
            // o o ↑
            // o o o
            for (x = cx + cycle, y = cy - cycle + 1; y <= cy + cycle; y += 1) {
                action(new Vector2Int(x, y));
                count++;
            }
            // o ← o
            // o o o
            // o o o
            for (x = cx + cycle - 1, y = cy + cycle; x >= cx + 1; x -= 1) {
                action(new Vector2Int(x, y));
                count++;
            }
            return count;
        }

        public static void SpiralRectCycle_OneCycleNearestGrid(in Vector2Int center, int cycle, Predicate<Vector2Int> condition) {
            int cx = center.x;
            int cy = center.y;
            int x;
            int y;
            // x ← o
            // o o o
            // o o o
            for (x = cx, y = cy + cycle; x >= cx - cycle; x -= 1) {
                if (condition(new Vector2Int(x, y))) {
                    return;
                }
            }
            // o o o
            // ↓ o o
            // x o o
            for (x = cx - cycle, y = cy + cycle - 1; y >= cy - cycle; y -= 1) {
                if (condition(new Vector2Int(x, y))) {
                    return;
                }
            }
            // o o o
            // o o o
            // o → x
            for (x = cx - cycle + 1, y = cy - cycle; x <= cx + cycle; x += 1) {
                if (condition(new Vector2Int(x, y))) {
                    return;
                }
            }
            // o o x
            // o o ↑
            // o o o
            for (x = cx + cycle, y = cy - cycle + 1; y <= cy + cycle; y += 1) {
                if (condition(new Vector2Int(x, y))) {
                    return;
                }
            }
            // o ← o
            // o o o
            // o o o
            for (x = cx + cycle - 1, y = cy + cycle; x >= cx + 1; x -= 1) {
                if (condition(new Vector2Int(x, y))) {
                    return;
                }
            }
        }

        /*
           gridCount == 0
           o

           gridCount == 1
           o o o 
           o o o
           o o o

           gridCount == 2
           o o o o o 
           o o o o o 
           o o o o o 
           o o o o o 
           o o o o o 
           return 2 * gridCount + 1
        */
        public static int SpiralRectCycle_GridCount(int gridCount) {
            int v = 2 * gridCount + 1;
            return v * v;
        }

        public static bool RectCycle_IsPosInside(Vector2Int center, int supplyGridCount, Vector2Int pos) {
            return Mathf.Abs(pos.x - center.x) <= supplyGridCount
                && Mathf.Abs(pos.y - center.y) <= supplyGridCount;
        }

        public static int CircleCycle_Grids(Vector2Int center, float radius, out Vector2Int[] cells) {
            cells = tempArray;
            if (radius <= 0) {
                cells[0] = center;
                return 1;
            }
            int count = 0;
            int range = Mathf.CeilToInt(radius);
            int rangeSqr = range * range;
            for (int x = center.x - range; x <= center.x + range; x++) {
                for (int y = center.y - range; y <= center.y + range; y++) {
                    Vector2 pos = new Vector2(x, y);
                    if (Vector2.SqrMagnitude(pos - center) <= rangeSqr) {
                        tempArray[count] = new Vector2Int(x, y);
                        count++;
                    }
                }
            }
            cells = tempArray;
            return count;
        }

    }
}