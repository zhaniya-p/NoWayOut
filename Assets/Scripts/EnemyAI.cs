using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    private AudioSource audioSource;

    [Header("Detection")]
    public float chaseDistance = 5f;
    public float catchDistance = 1.5f;
    public float loseDistance = 8f;

    [Header("Patrol")]
    public Transform[] waypoints;
    private int waypointIndex = 0;

    [Header("Speeds")]
    public float patrolSpeed = 1.5f;
    public float chaseSpeed = 3.5f;

    [Header("Audio")]
    public AudioClip chaseSound;

    private enum State { Patrol, Chase }
    private State currentState = State.Patrol;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        agent.speed = patrolSpeed;

        if (waypoints.Length > 0)
            agent.SetDestination(waypoints[0].position);
    }

    void Update()
    {
        float distToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                if (distToPlayer < chaseDistance)
                {
                    currentState = State.Chase;
                    agent.speed = chaseSpeed;
                }
                break;

            case State.Chase:
                Chase();
                if (audioSource != null && chaseSound != null && !audioSource.isPlaying)
                {
                    audioSource.clip = chaseSound;
                    audioSource.Play();
                }
                if (distToPlayer > loseDistance)
                {
                    currentState = State.Patrol;
                    agent.speed = patrolSpeed;
                    if (audioSource != null) audioSource.Stop();
                }
                if (distToPlayer < catchDistance)
                    CatchPlayer();
                break;
        }

        if (animator != null)
            animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    void Patrol()
    {
        if (waypoints.Length == 0) return;

        if (agent.remainingDistance < 1f && !agent.pathPending)
        {
            waypointIndex = (waypointIndex + 1) % waypoints.Length;
            agent.SetDestination(waypoints[waypointIndex].position);
        }
    }

    void Chase()
    {
        agent.SetDestination(player.position);
    }

    void CatchPlayer()
    {
        if (audioSource != null) audioSource.Stop();
        GameManager.Instance.TriggerLose();
    }
}