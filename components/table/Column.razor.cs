﻿using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace AntDesign
{
    public partial class Column<TData> : AntDomComponentBase, ITableColumn
    {
        [CascadingParameter]
        public ITable Table { get; set; }

        [CascadingParameter(Name = "IsHeader")]
        public bool IsHeader { get; set; }

        [CascadingParameter(Name = "InColGroup")]
        public bool InColGroup { get; set; }

        [CascadingParameter(Name = "IsPlaceholder")]
        public bool IsPlaceholder { get; set; }

        [CascadingParameter(Name = "Index")]
        public int Index { get; set; }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public TData Field { get; set; }

        [Parameter]
        public string Width { get; set; }

        [Parameter]
        public EventCallback<TData> FieldChanged { get; set; }

        [Parameter]
        public Expression<Func<TData>> FieldExpression { get; set; }

        [Parameter]
        public bool Sort { get; set; }

        [Parameter]
        public RenderFragment<TData> CellRender { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public bool Selected { get; set; }

        private FieldIdentifier? _fieldIdentifier;

        public string DisplayName => _fieldIdentifier?.GetDisplayName();

        public string FieldName => _fieldIdentifier?.FieldName;

        private void SetClass()
        {
            ClassMapper
                .If("ant-table-cell", () => IsHeader)
                ;
        }

        protected override void OnInitialized()
        {
            if (FieldExpression != null)
            {
                _fieldIdentifier = FieldIdentifier.Create(FieldExpression);
            }

            Table?.AddColumn(this);
            SetClass();
        }
    }
}
