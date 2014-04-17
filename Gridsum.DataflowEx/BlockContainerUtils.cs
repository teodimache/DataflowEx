﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Gridsum.DataflowEx.AutoCompletion;
using Microsoft.CSharp.RuntimeBinder;

namespace Gridsum.DataflowEx
{
    public static class BlockContainerUtils
    {
        public static BlockContainer<TIn, TOut> FromBlock<TIn, TOut>(IPropagatorBlock<TIn, TOut> block)
        {
            return new PropagatorBlockContainer<TIn, TOut>(block);
        }

        public static BlockContainer<TIn, TOut> FromBlock<TIn, TOut>(IPropagatorBlock<TIn, TOut> block, BlockContainerOptions options)
        {
            return new PropagatorBlockContainer<TIn, TOut>(block, options);
        }

        public static BlockContainer<TIn, TOut> AutoComplete<TIn, TOut>(this BlockContainer<TIn, TOut> blockContainer, TimeSpan timeout)
            where TIn : ITracableItem
            where TOut : ITracableItem 
        {
            var autoCompletePair = new AutoCompleteContainerPair<TIn, TOut>(timeout);

            var merged = new BlockContainerMerger<TIn, TIn, TOut, TOut>(
                autoCompletePair.Before, 
                blockContainer,
                autoCompletePair.After);

            return merged;
        }
    }
}
