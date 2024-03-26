module FSharp.EventSourcing.ToDo.Domain.Decider

open FSharp.EventSourcing.ToDo.Domain.Types
open FSharp.EventSourcing.ToDo.Domain.Functions

type Decider<'C, 'E, 'S, 'ERR> =
    { decide: 'C -> 'S -> Result<'E, 'ERR>
      evolve: 'S -> 'E -> 'S
      initialState: 'S
      isComplete: 'S -> bool }

let decide (command: TaskCommand) (state: TaskState) =
    match command with
    | CreateTask c -> handleCreateTaskCommand c state
    | AssignTask c -> handleAssignTaskCommand c state
    | AddComment c -> handleAddCommentCommand c state
    | CompleteTask c -> handleCompleteTaskCommand c state

let evolve (state: TaskState) (event: TaskEvent) =
    match state, event with
    | Empty, TaskCreated e -> createTask e
    | Open task, TaskCompleted e -> completeTask task e
    | Open task, _ -> TaskState.Open <| applyEvent task event 
    | _ -> state

let isComplete (state: TaskState) =
    match state with
    | Completed _ -> true
    | _ -> false

let TaskDecider =
    { decide = decide
      evolve = evolve
      initialState = Empty
      isComplete = isComplete }
