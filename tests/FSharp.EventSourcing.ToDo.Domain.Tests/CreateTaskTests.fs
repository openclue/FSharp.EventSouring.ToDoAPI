module FSharp.EventSourcing.ToDo.Domain.Tests.CreateTaskTests

open System
open FsUnit.Xunit
open FSharp.EventSourcing.ToDo.Domain
open FSharp.EventSourcing.ToDo.Domain.Types
open FSharp.EventSourcing.ToDo.Domain.Features
open Xunit

let initialState = Task.decider.initialState

let cmd: CreateTaskCommand =
    { Id = TaskId <| Guid.NewGuid()
      Title = "Test task"
      Description = "This is a test task"
      Priority = TaskPriority.Medium
      AuthorId = UserId <| Guid.NewGuid()
      Date = DateTimeOffset.Now }

let taskCreatedEvent: TaskEvent =
    TaskCreated
        { Title = cmd.Title
          Description = cmd.Description
          Priority = cmd.Priority
          AuthorId = cmd.AuthorId
          TaskId = cmd.Id
          CreatedAt = cmd.Date }

[<Fact>]
let ``Given CreateTaskCommand When createTask Then valid event emitted`` () =

    let event = createTask cmd

    match event with
    | Ok e -> e |> should equal taskCreatedEvent 
    | Error e -> failwith e


[<Fact>]
let ``Given TaskCreatedEvent When apply event Then valid state created`` () =

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

    let state = taskCreatedEvent |> applyEvent initialState

    state |> should equal expectedState
