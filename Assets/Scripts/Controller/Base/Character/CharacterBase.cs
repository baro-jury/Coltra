using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    #region Components
    [Header("Components")]
    protected Rigidbody2D rb;
    protected BoxCollider2D collid;
    protected GameObject currentOneWayPlatform;
    [SerializeField] protected List<GameObject> platformCantFall;
    #endregion

    #region Movement
    [Header("Movement")]
    public float speed = 200.0f;
    public float jumpForce = 18.0f;
    #endregion

    #region Ground Check
    [Header("Ground Check")]
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected Transform groundCheck;
    #endregion

    [Header("Color")]
    public Color? currentColor = null;
}
