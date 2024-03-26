module FSharp.EventSourcing.ToDo.Domain.Tests.CreateTaskTests

open System
open FsUnit.Xunit
open FSharp.EventSourcing.ToDo.Domain
open FSharp.EventSourcing.ToDo.Domain.Types
open Xunit

let taskDecider = Task.decider

let cmd: CreateTaskCommand =
    { Id = TaskId <| Guid.NewGuid()
      Title = "Test task"
      Description = "This is a test task"
      Priority = TaskPriority.Medium
      AuthorId = UserId <| Guid.NewGuid()
      Date = DateTimeOffset.Now }

[<Fact>]
let ``Given CreateTask command When Decider decide Then valid event emitted`` () =

    let expectedEvent: TaskEvent =
        TaskCreated
            { Title = cmd.Title
              Description = cmd.Description
              Priority = cmd.Priority
              AuthorId = cmd.AuthorId
              TaskId = cmd.Id
              CreatedAt = cmd.Date }

    let event =
        TaskCommand.CreateTask cmd |> taskDecider.decide taskDecider.initialState

    match event with
    | Ok e -> e |> should equal expectedEvent
    | Error e -> failwith e


[<Fact>]
let ``Given CreateTask command When Decider decide and evolve Then valid state created`` () =

    let expectedState: TaskState =
        Open
            { Id = cmd.Id
              Assigment = None
              Comments = List.Empty
              Title = cmd.Title
              Description = cmd.Description
              Priority = cmd.Priority
              AuthorId = cmd.AuthorId
              CreatedAt = cmd.Date }

    let state =
        TaskCommand.CreateTask cmd
        |> Task.decider.decide Task.decider.initialState
        |> Result.map (taskDecider.evolve taskDecider.initialState)


    match state with
    | Ok s -> s |> should equal expectedState
    | Error e -> failwith e
