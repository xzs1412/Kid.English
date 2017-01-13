using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Castle.Core.Internal;
using Kid.English.Phrases;

namespace Kid.English.EntityFramework.Repositories
{
    public class PhraseRepository : EnglishRepositoryBase<Phrase, Guid>, IPhraseRepository
    {
        public PhraseRepository(IDbContextProvider<EnglishDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        /// <summary>
        ///     Search phrase by keywords conditions.
        /// </summary>
        /// <param name="count">The search result's record count</param>
        /// <param name="englishKeyWords"></param>
        /// <param name="chineseKeyWords"></param>
        /// <param name="pageIndex">base on 1,default:1</param>
        /// <param name="pageSize">default:10</param>
        /// <returns></returns>
        [Obsolete("This method is obsolete;use async version named SearchPhrasesAsync instead",true)]
        public List<Phrase> SearchPhrases(out int count, string englishKeyWords = "", string chineseKeyWords = "",
            int pageIndex = 1, int pageSize = 10)
        {
            //按匹配相关度排序
            //select * from [user]  where 
            //[name] like '%a%' or  [name] like '%b%'  or [Name]  like '%c%'or  [Name]  like '%d%' or [Name]  like '%e%' or  [Name]  like '%f%'  order by 
            //(case when [Name] like '%a%' then 1 else 0 end) +
            //(case when  [Name]  like '%b%' then 1 else 0 end) +
            //(case when  [Name]  like '%c%' then 1 else 0 end) +
            //(case when  [Name]  like '%d%' then 1 else 0 end)+
            //(case when  [Name]  like '%e%' then 1 else 0 end) +
            //(case when  [Name]  like '%f%' then 1 else 0 end) desc
            //using (var context=this.GetDbContext())
            //{

            //}

            using (var context = new EnglishDbContext())
            {
                var sqlStringList = "select * from phrases where {0} order by {1} desc";
                var sqlStringCount = "select count(ID) from phrases where {0}";
                string sqlWhere = string.Empty, sqlOrderBy = string.Empty;

                //这两个参数其实一样,但是两个查询必须用两套参数,不然异常:"其他信息: 另一个 SqlParameterCollection 中已包含 SqlParameter。"
                //保存查询结果的参数
                var paramList = new List<SqlParameter>();
                //保存查询总数的参数
                var paramCountList = new List<SqlParameter>();

                if (!string.IsNullOrEmpty(englishKeyWords))
                {
                    var englishKeyWordArray =
                        englishKeyWords.Split(new[] {" ", " ", ",", "，", ";", "；"},
                            StringSplitOptions.RemoveEmptyEntries);
                    for (var i = 0; i < englishKeyWordArray.Length; i++)
                    {
                        if (string.IsNullOrEmpty(sqlWhere))
                        {
                            sqlWhere = "[Sentence] like @EnglishKeyWord" + i;
                            //后一个case用来全字匹配一个单词，只影响排序。
                            sqlOrderBy =
                                string.Format(
                                    "(case when [Sentence] like @EnglishKeyWord{0} then 1 else 0 end)+(case when ' '+[Sentence]+' ' like @EnglishKeyWord{0}_{0} then 100 else 0 end)",
                                    i);
                        }
                        else
                        {
                            sqlWhere += " or [Sentence] like @EnglishKeyWord" + i;
                            //后一个case用来全字匹配一个单词，只影响排序。
                            sqlOrderBy +=
                                string.Format(
                                    "+(case when [Sentence] like @EnglishKeyWord{0} then 1 else 0 end)+(case when ' '+[Sentence]+' ' like @EnglishKeyWord{0}_{0} then 100 else 0 end)",
                                    i);
                        }
                        paramCountList.Add(new SqlParameter("@EnglishKeyWord" + i, "%" + englishKeyWordArray[i] + "%"));
                        paramList.Add(new SqlParameter("@EnglishKeyWord" + i, "%" + englishKeyWordArray[i] + "%"));
                        paramList.Add(new SqlParameter(string.Format("@EnglishKeyWord{0}_{0}", i),
                            $"%[^a-zA-Z]{englishKeyWordArray[i]}[^a-zA-Z]%"));
                    }
                }

                if (!string.IsNullOrEmpty(chineseKeyWords))
                {
                    var chineseKeyWordArray =
                        chineseKeyWords.Split(new[] {" ", " ", ",", "，", ";", "；"},
                            StringSplitOptions.RemoveEmptyEntries);
                    for (var i = 0; i < chineseKeyWordArray.Length; i++)
                    {
                        if (string.IsNullOrEmpty(sqlWhere))
                        {
                            sqlWhere = "[ChineseMean] like @chineseKeyWord" + i;
                            sqlOrderBy = "(case when [ChineseMean] like @ChineseKeyWord" + i + " then 1 else 0 end)";
                        }
                        else
                        {
                            sqlWhere += " or [ChineseMean] like @ChineseKeyWord" + i;
                            sqlOrderBy += "+(case when [ChineseMean] like @ChineseKeyWord" + i + " then 1 else 0 end)";
                        }

                        paramCountList.Add(new SqlParameter("@ChineseKeyWord" + i, "%" + chineseKeyWordArray[i] + "%"));
                        paramList.Add(new SqlParameter("@ChineseKeyWord" + i, "%" + chineseKeyWordArray[i] + "%"));
                    }
                }

                DbRawSqlQuery<Phrase> sqlQuery;
                DbRawSqlQuery<int> sqlCountQuery;

                const string filterTheSoftDeletedData = "IsDeleted=0";

                if (string.IsNullOrEmpty(sqlWhere))
                {
                    //查询记录数量(doesn't contain the soft-deleted data)
                    sqlStringCount = string.Format(sqlStringCount, filterTheSoftDeletedData);
                    sqlCountQuery = context.Database.SqlQuery<int>(sqlStringCount);

                    //查询记录(doesn't contain the soft-deleted data)
                    sqlStringList = string.Format(sqlStringList, filterTheSoftDeletedData, "[CreationTime]");
                    sqlQuery = context.Database.SqlQuery<Phrase>(sqlStringList);
                }
                else
                {
                    //doesn't contain the soft-deleted data
                    sqlWhere += " and " + filterTheSoftDeletedData;

                    sqlStringCount = string.Format(sqlStringCount, sqlWhere);
                    sqlCountQuery = context.Database.SqlQuery<int>(sqlStringCount, paramCountList);

                    sqlStringList = string.Format(sqlStringList, sqlWhere, sqlOrderBy) + ",[CreationTime] desc";
                    sqlQuery = context.Database.SqlQuery<Phrase>(sqlStringList, paramList);
                }

                count = sqlCountQuery.First();

                var list = sqlQuery.Skip(pageSize*(pageIndex - 1)).Take(pageSize).ToList();
                return list;
            } //end using
        }

        public async Task<Tuple<int, List<Phrase>>> SearchPhrasesAsync(string englishKeyWords, string chineseKeyWords,
            int pageIndex, int pageSize)
        {
            //Context
            //using (Context context = new EnglishDbContext())
            //{
            string sqlWhere = string.Empty, sqlOrderBy = string.Empty;

            //这两个参数其实一样,但是两个查询必须用两套参数,不然异常:"其他信息: 另一个 SqlParameterCollection 中已包含 SqlParameter。"
            //保存查询结果的参数
            var paramList = new List<SqlParameter>();
            //保存查询总数的参数
            var paramCountList = new List<SqlParameter>();

            var splitStringArray = new[] {" ", " ", ",", "，", ";", "；"};
            if (!string.IsNullOrEmpty(englishKeyWords))
            {
                var englishKeyWordArray =
                    englishKeyWords.Split(splitStringArray,
                        StringSplitOptions.RemoveEmptyEntries);
                for (var i = 0; i < englishKeyWordArray.Length; i++)
                {
                    if (string.IsNullOrEmpty(sqlWhere))
                    {
                        sqlWhere = "[Sentence] like @EnglishKeyWord" + i;
                        //后一个case用来全字匹配一个单词，只影响排序。
                        sqlOrderBy =
                            $"(case when [Sentence] like @EnglishKeyWord{i} then 1 else 0 end)+(case when ' '+[Sentence]+' ' like @EnglishKeyWord{i}_{i} then 100 else 0 end)";
                    }
                    else
                    {
                        sqlWhere += $" or [Sentence] like @EnglishKeyWord{i}";
                        //后一个case用来全字匹配一个单词，只影响排序。
                        sqlOrderBy +=
                            $"+(case when [Sentence] like @EnglishKeyWord{i} then 1 else 0 end)+(case when ' '+[Sentence]+' ' like @EnglishKeyWord{i}_{i} then 100 else 0 end)";
                    }
                    paramCountList.Add(new SqlParameter($"@EnglishKeyWord{i}", $"%{englishKeyWordArray[i]}%"));
                    paramList.Add(new SqlParameter($"@EnglishKeyWord{i}", $"%{englishKeyWordArray[i]}%"));
                    paramList.Add(new SqlParameter($"@EnglishKeyWord{i}_{i}",
                        $"%[^a-zA-Z]{englishKeyWordArray[i]}[^a-zA-Z]%"));
                }
            }

            if (!string.IsNullOrEmpty(chineseKeyWords))
            {
                var chineseKeyWordArray =
                    chineseKeyWords.Split(splitStringArray,
                        StringSplitOptions.RemoveEmptyEntries);
                for (var i = 0; i < chineseKeyWordArray.Length; i++)
                {
                    if (string.IsNullOrEmpty(sqlWhere))
                    {
                        sqlWhere = $"[ChineseMean] like @chineseKeyWord{i}";
                        sqlOrderBy = $"(case when [ChineseMean] like @ChineseKeyWord{i} then 1 else 0 end)";
                    }
                    else
                    {
                        sqlWhere += $" or [ChineseMean] like @ChineseKeyWord{i}";
                        sqlOrderBy += $"+(case when [ChineseMean] like @ChineseKeyWord{i} then 1 else 0 end)";
                    }

                    paramCountList.Add(new SqlParameter($"@ChineseKeyWord{i}", $"%{chineseKeyWordArray[i]}%"));
                    paramList.Add(new SqlParameter($"@ChineseKeyWord{i}", $"%{chineseKeyWordArray[i]}%"));
                }
            }

            DbRawSqlQuery<Phrase> sqlQueryList;
            DbRawSqlQuery<int> sqlQueryCount;

            var sqlStringCount = GetSqlStringCount(sqlWhere);
            var sqlStringList = GetSqlStringList(sqlWhere, sqlOrderBy, pageIndex, pageSize);

            if (string.IsNullOrEmpty(sqlWhere))
            {
                sqlQueryCount = Context.Database.SqlQuery<int>(sqlStringCount);
                sqlQueryList = Context.Database.SqlQuery<Phrase>(sqlStringList);
            }
            else
            {
                sqlQueryCount = Context.Database.SqlQuery<int>(sqlStringCount, paramCountList.ToArray());
                sqlQueryList = Context.Database.SqlQuery<Phrase>(sqlStringList, paramList.ToArray());
            }

            var count = await sqlQueryCount.FirstAsync();
            var list = await sqlQueryList.ToListAsync();

            return new Tuple<int, List<Phrase>>(count, list);
            // } //end using
        }

        private string GetSqlStringList(string sqlWhere, string sqlOrderBy, int pageIndex, int pageSize)
        {
            if ((sqlWhere.IsNullOrEmpty() && !sqlOrderBy.IsNullOrEmpty())
                || (!sqlWhere.IsNullOrEmpty() && sqlOrderBy.IsNullOrEmpty()))
                throw new ArgumentException($"{nameof(sqlWhere)} and {nameof(sqlOrderBy)} 两个参数不能一个为空一个不为空",
                    nameof(GetSqlStringList));

            const string filterTheSoftDeletedData = "IsDeleted=0";
            const string creationTime = "[CreationTime] desc";

            var rowNoStart = (pageIndex - 1)*pageSize;
            var rowNoEnd = pageIndex*pageSize;

            if (string.IsNullOrEmpty(sqlWhere))
            {
                sqlOrderBy = creationTime;
                sqlWhere = filterTheSoftDeletedData;
                //查询记录(doesn't contain the soft-deleted data)
            }
            else
            {
                sqlOrderBy += $" desc,{creationTime}";
                //doesn't contain the soft-deleted data
                sqlWhere += $" and {filterTheSoftDeletedData}";
            }
            return
                $"select * from( select *, row_number() over(order by {sqlOrderBy}) as RowNo from phrases where {sqlWhere}) as T0 where RowNo>{rowNoStart} and rowNo<={rowNoEnd}";
        }

        private string GetSqlStringCount(string sqlWhere)
        {
            const string filterTheSoftDeletedData = "IsDeleted=0";

            if (string.IsNullOrEmpty(sqlWhere))
                sqlWhere = filterTheSoftDeletedData;
            else
                sqlWhere += $" and {filterTheSoftDeletedData}";
            return $"select count(ID) from phrases where {sqlWhere}";
        }

        #region "No used"

        //    private List<Phrase> SearchPhrasesExtension(out int count, string englishKeyWords = "", string chineseKeyWords = "", int pageIndex = 1, int pageSize = 10)
        //    {
        //        //按匹配相关度排序
        //        //select * from [user]  where 
        //        //[name] like '%a%' or  [name] like '%b%'  or [Name]  like '%c%'or  [Name]  like '%d%' or [Name]  like '%e%' or  [Name]  like '%f%'  order by 
        //        //(case when [Name] like '%a%' then 1 else 0 end) +
        //        //(case when  [Name]  like '%b%' then 1 else 0 end) +
        //        //(case when  [Name]  like '%c%' then 1 else 0 end) +
        //        //(case when  [Name]  like '%d%' then 1 else 0 end)+
        //        //(case when  [Name]  like '%e%' then 1 else 0 end) +
        //        //(case when  [Name]  like '%f%' then 1 else 0 end) desc

        //        using (EnglishDbContext context = new EnglishDbContext())
        //        {
        //            IQueryable<Phrase> phraseList=from p in GetAll() select p;
        //            IQueryable<int> totalCount=from p in GetAll() select p.c;

        //            string sqlStringList = "select * from phrases where {0} order by {1} desc";
        //            string sqlStringCount = "select count(ID) from phrases where {0}";
        //            string sqlWhere = string.Empty, sqlOrderBy = string.Empty;

        //            //这两个参数其实一样,但是两个查询必须用两套参数,不然异常:"其他信息: 另一个 SqlParameterCollection 中已包含 SqlParameter。"
        //            //保存查询结果的参数
        //            List<SqlParameter> paramList = new List<SqlParameter>();
        //            //保存查询总数的参数
        //            List<SqlParameter> paramCountList = new List<SqlParameter>();

        //            if (!string.IsNullOrEmpty(englishKeyWords))
        //            {
        //                string[] englishKeyWordArray =
        //                    englishKeyWords.Split(new string[] { " ", " ", ",", "，", ";", "；" },
        //                    StringSplitOptions.RemoveEmptyEntries);
        //                for (int i = 0; i < englishKeyWordArray.Length; i++)
        //                {
        //                    if (string.IsNullOrEmpty(sqlWhere))
        //                    {
        //                        phraseList.Where(p => p.Sentence.Contains(englishKeyWordArray[i]));


        //                        //后一个case用来全字匹配一个单词，只影响排序。
        //                        sqlOrderBy = string.Format("(case when [Sentence] like @EnglishKeyWord{0} then 1 else 0 end)+(case when ' '+[Sentence]+' ' like @EnglishKeyWord{0}_{0} then 100 else 0 end)", i);
        //                    }
        //                    else
        //                    {
        //                        sqlWhere += " or [Sentence] like @EnglishKeyWord" + i;
        //                        //后一个case用来全字匹配一个单词，只影响排序。
        //                        sqlOrderBy += string.Format("+(case when [Sentence] like @EnglishKeyWord{0} then 1 else 0 end)+(case when ' '+[Sentence]+' ' like @EnglishKeyWord{0}_{0} then 100 else 0 end)", i);
        //                    }
        //                    paramCountList.Add(new SqlParameter("@EnglishKeyWord" + i, "%" + englishKeyWordArray[i] + "%"));
        //                    paramList.Add(new SqlParameter("@EnglishKeyWord" + i, "%" + englishKeyWordArray[i] + "%"));
        //                    paramList.Add(new SqlParameter(string.Format("@EnglishKeyWord{0}_{0}", i),
        //                        string.Format("%[^a-zA-Z]{0}[^a-zA-Z]%", englishKeyWordArray[i])));
        //                }
        //            }

        //            if (!string.IsNullOrEmpty(chineseKeyWords))
        //            {
        //                string[] chineseKeyWordArray =
        //                    chineseKeyWords.Split(new string[] { " ", " ", ",", "，", ";", "；" },
        //                    StringSplitOptions.RemoveEmptyEntries);
        //                for (int i = 0; i < chineseKeyWordArray.Length; i++)
        //                {
        //                    if (string.IsNullOrEmpty(sqlWhere))
        //                    {
        //                        sqlWhere = "[ChineseMean] like @chineseKeyWord" + i;
        //                        sqlOrderBy = "(case when [ChineseMean] like @ChineseKeyWord" + i + " then 1 else 0 end)";
        //                    }
        //                    else
        //                    {
        //                        sqlWhere += " or [ChineseMean] like @ChineseKeyWord" + i;
        //                        sqlOrderBy += "+(case when [ChineseMean] like @ChineseKeyWord" + i + " then 1 else 0 end)";
        //                    }

        //                    paramCountList.Add(new SqlParameter("@ChineseKeyWord" + i, "%" + chineseKeyWordArray[i] + "%"));
        //                    paramList.Add(new SqlParameter("@ChineseKeyWord" + i, "%" + chineseKeyWordArray[i] + "%"));
        //                }
        //            }

        //            DbRawSqlQuery<Phrase> sqlQuery;
        //            DbRawSqlQuery<int> sqlCountQuery;

        //            const string filterTheSoftDeletedData = "IsDeleted=0";

        //            if (string.IsNullOrEmpty(sqlWhere))
        //            {

        //                //查询记录数量(doesn't contain the soft-deleted data)
        //                sqlStringCount = string.Format(sqlStringCount, filterTheSoftDeletedData);
        //                sqlCountQuery = context.Database.SqlQuery<int>(sqlStringCount);

        //                //查询记录(doesn't contain the soft-deleted data)
        //                sqlStringList = string.Format(sqlStringList, filterTheSoftDeletedData, "[CreationTime]");
        //                sqlQuery = context.Database.SqlQuery<Phrase>(sqlStringList);
        //            }
        //            else
        //            {
        //                //doesn't contain the soft-deleted data
        //                sqlWhere += " and " + filterTheSoftDeletedData;

        //                sqlStringCount = string.Format(sqlStringCount, sqlWhere);
        //                sqlCountQuery = context.Database.SqlQuery<int>(sqlStringCount, paramCountList.ToArray());

        //                sqlStringList = string.Format(sqlStringList, sqlWhere, sqlOrderBy) + ",[CreationTime] desc";
        //                sqlQuery = context.Database.SqlQuery<Phrase>(sqlStringList, paramList.ToArray());
        //            }

        //            count = sqlCountQuery.First<int>();

        //            List<Phrase> list = sqlQuery.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
        //            return list;
        //        }//end using
        //    }

        #endregion
    }
}