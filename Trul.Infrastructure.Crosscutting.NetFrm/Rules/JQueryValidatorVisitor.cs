using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Trul.Framework.Rules;

namespace Trul.Infrastructure.Crosscutting.NetFramework.Rules
{
    public class JQueryValidatorVisitor : IClientValidatorVisitor
    {
        private string _formId;

        private readonly Dictionary<string, StringBuilder> _rules = new Dictionary<string, StringBuilder>();
        private readonly Dictionary<string, StringBuilder> _messages = new Dictionary<string, StringBuilder>();

        private IRule _currentRule;

        private StringBuilder _currentRules;
        private StringBuilder _currentMessages;

        public void Visit(IRule rule)
        {
            _currentRule = rule;

            rule.Constraint.Accept(this);
        }

        public void Visit<T>(PropertyValueConstraint<T> constraint)
        {
            //var fieldName = typeof(T).Name + "$" + ((MemberExpression)constraint.FieldExpression.Body).Member.Name;
            var fieldName = string.Empty;
            if (constraint.FieldExpression.Body is MemberExpression)
            {
                fieldName = ((MemberExpression)constraint.FieldExpression.Body).Member.Name;
            }
            else
            {
                var op = ((UnaryExpression)constraint.FieldExpression.Body).Operand;
                fieldName = ((MemberExpression)op).Member.Name;
            }  

            if (!_rules.ContainsKey(fieldName))
                _rules.Add(fieldName, new StringBuilder());

            if (!_messages.ContainsKey(fieldName))
                _messages.Add(fieldName, new StringBuilder());

            _currentRules = _rules[fieldName];
            _currentMessages = _messages[fieldName];

            constraint.InnerConstraint.Accept(this);
        }

        public void Visit<T>(PropertiesValueConstraint<T> constraint)
        {
            if (!_rules.ContainsKey(constraint.FieldName))
                _rules.Add(constraint.FieldName, new StringBuilder());

            if (!_messages.ContainsKey(constraint.FieldName))
                _messages.Add(constraint.FieldName, new StringBuilder());

            _currentRules = _rules[constraint.FieldName];
            _currentMessages = _messages[constraint.FieldName];

            constraint.InnerConstraint.Accept(this);
        }

        public void Visit(StringMaxLengthConstraint constraint)
        {
            _currentRules.Append("maxlength:" + constraint.MaxLength + ",");
            _currentMessages.Append("maxlength:\"" + _currentRule.Message + "\",");
        }

        public void Visit(EmailConstraint constraint)
        {
            _currentRules.Append("email:true,");
            _currentMessages.Append("email:\"" + _currentRule.Message + "\",");
        }

        public void Visit(StringNotNullOrEmptyConstraint constraint)
        {
            _currentRules.Append("required:true,");
            _currentMessages.Append("required:\"" + _currentRule.Message + "\",");
        }

        public void Visit(EqualToConstraint constraint)
        {
            _currentRules.AppendFormat("equalTo: \"#{0}\"\"", (_currentRule.Constraint as ICompareField).RightFieldName);
             _currentMessages.Append("equalTo:\"" + _currentRule.Message + "\",");
        }

        public string VisitValidator(IEnumerable<IRulesGroup> rulesGroups, ValidateSettings settings)
        {
            if (string.IsNullOrWhiteSpace(settings.FormID)) throw new ArgumentNullException();

            var builder = new StringBuilder();
            _formId = settings.FormID;

            foreach (var validator in rulesGroups)
                foreach (var rule in validator.Rules)
                    rule.Accept(this);

            builder.AppendFormat("$(document).ready(function(){{$(\"#{0}\").validate({{", _formId);

            if (!string.IsNullOrWhiteSpace(settings.ErrorContainer)) builder.AppendFormat("errorContainer: \"#{0}\",", settings.ErrorContainer);
            if (!string.IsNullOrWhiteSpace(settings.ErrorLabelContainer)) builder.AppendFormat("errorLabelContainer: \"#{0}\",", settings.ErrorLabelContainer);
            if (!string.IsNullOrWhiteSpace(settings.Wrapper)) builder.AppendFormat("wrapper: \"{0}\",", settings.Wrapper);
            if (!string.IsNullOrWhiteSpace(settings.ErrorClass)) builder.AppendFormat("errorClass: \"{0}\",", settings.ErrorClass);


            builder.Append("rules:{");

            foreach (var rule in _rules)
            {
                builder.AppendFormat("{0}:{{{1}", rule.Key, rule.Value);
                builder.Remove(builder.Length - 1, 1);
                builder.Append("},");
            }

            builder.Remove(builder.Length - 1, 1);

            builder.Append("},messages: {");

            foreach (var message in _messages)
            {
                builder.AppendFormat("{0}:{{{1}", message.Key, message.Value);
                builder.Remove(builder.Length - 1, 1);
                builder.Append("},");
            }

            builder.Remove(builder.Length - 1, 1);

            builder.Append("}});");

            if (!string.IsNullOrWhiteSpace(settings.SubmitControlID))
            {
                builder.AppendFormat("$(\"#{0}\").click(function(){{", settings.SubmitControlID);
                builder.AppendFormat("$(\"#{0}\").validate();", _formId);
                builder.Append("});");
            }

            builder.Append("});");

            return string.Format("<script type=\"text/javascript\">\n{0}</script>", builder);
        }
    }
}