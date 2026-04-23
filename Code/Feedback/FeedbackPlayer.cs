using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FeedbackPlayer : MonoBehaviour
{
    private List<Feedback> feedbackList;
    private void Awake()
    {
        feedbackList = GetComponentsInChildren<Feedback>().ToList();
    }

    public void PlayFeedbacks()
    {
        feedbackList.ForEach(f => f.PlayFeedback());
    }
}
