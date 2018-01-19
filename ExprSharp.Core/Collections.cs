using iExpr;
using iExpr.Evaluators;
using iExpr.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExprSharp
{
    [CanClassValue(Name = "stack")]
    public class Stack
    {
        Stack<IValue> stack = new Stack<IValue>();

        [ClassMethod(Name = "count", ArgumentCount = 0, IsReadOnly = true)]
        public int Count(FunctionArgument _args, EvalContext cal)
        {
            return stack.Count;
        }

        [ClassMethod(Name = "top", ArgumentCount = 0, IsReadOnly = true)]
        public IValue Top(FunctionArgument _args, EvalContext cal)
        {
            return stack.Peek();
        }

        [ClassMethod(Name = "pop", ArgumentCount = 0,IsReadOnly =true)]
        public IValue Pop(FunctionArgument _args, EvalContext cal)
        {
            return stack.Pop();
        }

        [ClassMethod(Name = "push", ArgumentCount = 1, IsReadOnly = true)]
        public void Push(FunctionArgument _args, EvalContext cal)
        {
            OperationHelper.AssertArgsNumberThrowIf(this, 1, _args.Arguments);
            var v = cal.GetValue<IValue>(_args.Arguments[0]);
            stack.Push(v);
        }

        [ClassCtorMethod(ArgumentCount = 0)]
        public Stack(FunctionArgument _args, EvalContext cal)
        {
            
        }
    }

    [CanClassValue(Name = "queue")]
    public class Queue
    {
        Queue<IValue> stack = new Queue<IValue>();

        [ClassMethod(Name = "count", ArgumentCount = 0, IsReadOnly = true)]
        public int Count(FunctionArgument _args, EvalContext cal)
        {
            return stack.Count;
        }

        [ClassMethod(Name = "front", ArgumentCount = 0, IsReadOnly = true)]
        public IValue Front(FunctionArgument _args, EvalContext cal)
        {
            return stack.Peek();
        }

        [ClassMethod(Name = "pop", ArgumentCount = 0, IsReadOnly = true)]
        public IValue Pop(FunctionArgument _args, EvalContext cal)
        {
            return stack.Dequeue();
        }

        [ClassMethod(Name = "push", ArgumentCount = 1, IsReadOnly = true)]
        public void Push(FunctionArgument _args, EvalContext cal)
        {
            OperationHelper.AssertArgsNumberThrowIf(this, 1, _args.Arguments);
            var v = cal.GetValue<IValue>(_args.Arguments[0]);
            stack.Enqueue(v);
        }

        [ClassCtorMethod(ArgumentCount = 0)]
        public Queue(FunctionArgument _args, EvalContext cal)
        {

        }
    }
}
