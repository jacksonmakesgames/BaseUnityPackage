using System;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Jackson
{

    public struct ScreenEdges
    {
        public float maxY;
        public float minY;
        public float maxX;
        public float minX;
    }

    public class Utilities
    {
        public static ScreenEdges screenEdges;



        static void UpdateScreenEdges()
        {
            screenEdges = new ScreenEdges();

            RaycastHit hit;

            Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, 1.0f, 0));
            if (Physics.Raycast(ray, out hit))
                screenEdges.maxY = hit.point.y - hit.point.y / 4;

            ray = Camera.main.ViewportPointToRay(new Vector3(.5f, 0f, 0));
            if (Physics.Raycast(ray, out hit))
                screenEdges.minY = hit.point.y - hit.point.y / 4;

            ray = Camera.main.ViewportPointToRay(new Vector3(0.0f, .5f, 0));
            if (Physics.Raycast(ray, out hit))
                screenEdges.minX = hit.point.x - hit.point.x / 4;

            ray = Camera.main.ViewportPointToRay(new Vector3(1.0f, .5f, 0));
            if (Physics.Raycast(ray, out hit))
                screenEdges.maxX = hit.point.x - hit.point.x / 4;


            screenEdges.maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
            screenEdges.minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
            screenEdges.maxY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
            screenEdges.minY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;

        }

    }
}
