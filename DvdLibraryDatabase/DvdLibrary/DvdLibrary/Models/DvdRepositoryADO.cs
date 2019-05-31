using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DvdLibrary.Models
{
    public class DvdRepositoryADO : IDvdRepository
    {
        private string _connection = "Server=localhost;Database=DvdLibrary;User Id=DvdLibraryApp;Password=testing123;";

        public void Create(Dvd newDvd)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connection;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DvdInsert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Title", newDvd.Title);
                cmd.Parameters.AddWithValue("@Year", newDvd.ReleaseDate);
                cmd.Parameters.AddWithValue("@Director", newDvd.Director);
                cmd.Parameters.AddWithValue("@Rating", newDvd.Rating);
                cmd.Parameters.AddWithValue("@Notes", newDvd.Notes);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int dvdId)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connection;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DvdDelete";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DvdId", dvdId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Dvd Get(int dvdId)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connection;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DvdSelectById";
                cmd.Parameters.AddWithValue("@DvdId", dvdId);

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Dvd dvd = new Dvd();

                        dvd.DvdId = (int)dr["dvdId"];
                        dvd.Title = dr["title"].ToString();
                        dvd.ReleaseDate = dr["ReleaseDate"].ToString();
                        dvd.Director = dr["director"].ToString();
                        dvd.Rating = dr["rating"].ToString();

                        if (dr["Notes"] != DBNull.Value)
                            dvd.Notes = dr["Notes"].ToString();
                        return dvd;
                    }
                }
                return null;
            }
        }

        public IEnumerable<Dvd> GetAll()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connection;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DvdSelectAll";

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Dvd dvd = new Dvd();

                        dvd.DvdId = (int)dr["DvdId"];
                        dvd.Title = dr["Title"].ToString();
                        dvd.ReleaseDate = dr["ReleaseDate"].ToString();
                        dvd.Director = dr["Director"].ToString();
                        dvd.Rating = dr["Rating"].ToString();

                        if (dr["Notes"] != DBNull.Value)
                            dvd.Notes = dr["Notes"].ToString();
                        yield return dvd;
                    }
                }
                yield break;
            }
        }

        public IEnumerable<Dvd> GetByDirector(string director)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connection;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DvdSelectByDirector";
                cmd.Parameters.AddWithValue("@Director", director);

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Dvd dvd = new Dvd();

                        dvd.DvdId = (int)dr["DvdId"];
                        dvd.Title = dr["Title"].ToString();
                        dvd.ReleaseDate = dr["ReleaseDate"].ToString();
                        dvd.Director = dr["Director"].ToString();
                        dvd.Rating = dr["Rating"].ToString();

                        if (dr["Notes"] != DBNull.Value)
                            dvd.Notes = dr["Notes"].ToString();
                        yield return dvd;
                    }
                }
                yield break;
            }
        }

        public IEnumerable<Dvd> GetByRating(string rating)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connection;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DvdSelectByRating";
                cmd.Parameters.AddWithValue("@Rating", rating);

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Dvd dvd = new Dvd();

                        dvd.DvdId = (int)dr["DvdId"];
                        dvd.Title = dr["Title"].ToString();
                        dvd.ReleaseDate = dr["ReleaseDate"].ToString();
                        dvd.Director = dr["Director"].ToString();
                        dvd.Rating = dr["Rating"].ToString();

                        if (dr["Notes"] != DBNull.Value)
                            dvd.Notes = dr["Notes"].ToString();
                        yield return dvd;
                    }
                }
                yield break;
            }
        }

        public IEnumerable<Dvd> GetByTitle(string title)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connection;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DvdSelectByTitle";
                cmd.Parameters.AddWithValue("@Title", title);

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Dvd dvd = new Dvd();

                        dvd.DvdId = (int)dr["DvdId"];
                        dvd.Title = dr["Title"].ToString();
                        dvd.ReleaseDate = dr["ReleaseDate"].ToString();
                        dvd.Director = dr["Director"].ToString();
                        dvd.Rating = dr["Rating"].ToString();

                        if (dr["Notes"] != DBNull.Value)
                            dvd.Notes = dr["Notes"].ToString();
                        yield return dvd;
                    }
                }
                yield break;
            }
        }

        public IEnumerable<Dvd> GetByYear(string releaseYear)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connection;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DvdSelectByYear";
                cmd.Parameters.AddWithValue("@Year", releaseYear);

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Dvd dvd = new Dvd();

                        dvd.DvdId = (int)dr["DvdId"];
                        dvd.Title = dr["Title"].ToString();
                        dvd.ReleaseDate = dr["ReleaseDate"].ToString();
                        dvd.Director = dr["Director"].ToString();
                        dvd.Rating = dr["Rating"].ToString();

                        if (dr["Notes"] != DBNull.Value)
                            dvd.Notes = dr["Notes"].ToString();
                        yield return dvd;
                    }
                }
                yield break;
            }
        }

        public void Update(Dvd updatedDvd)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connection;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DvdUpdate";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DvdId", updatedDvd.DvdId);
                cmd.Parameters.AddWithValue("@Title", updatedDvd.Title);
                cmd.Parameters.AddWithValue("@Year", updatedDvd.ReleaseDate);
                cmd.Parameters.AddWithValue("@Director", updatedDvd.Director);
                cmd.Parameters.AddWithValue("@Rating", updatedDvd.Rating);
                cmd.Parameters.AddWithValue("@Notes", updatedDvd.Notes);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}